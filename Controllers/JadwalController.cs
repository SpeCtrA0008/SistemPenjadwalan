using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboardApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AdminDashboardApp.Controllers
{
    public sealed class TeknisiSelectVm
    {
        public int TeknisiId { get; set; }
        public string Nama { get; set; } = string.Empty;
        public int CountAcaraHariItu { get; set; }
    }

    
    public class JadwalController : Controller
    {
        private readonly AppDbContext _context;

        public JadwalController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string? hari, int? minggu, int? bulan, string? search)
        {
            var query = _context.jadwalteknisi
                .Include(j => j.TeknisiJadwals).ThenInclude(tj => tj.Teknisi)
                .Include(j => j.RuangRapat)
                .Include(j => j.JenisSupportJadwals).ThenInclude(jsj => jsj.JenisSupport)
                .Include(j => j.Fungsi)
                .Include(j => j.Bagian)
                .AsNoTracking()
                .AsQueryable();

            // Filter bulan masih bisa dijalankan di SQL
            if (bulan.HasValue)
            {
                query = query.Where(d => d.Tanggal.HasValue && d.Tanggal.Value.Month == bulan.Value);
            }

            // Pindah ke memory sebelum filter hari
            if (!string.IsNullOrWhiteSpace(hari))
            {
                query = query
                    .AsEnumerable()
                    .Where(d => d.Tanggal.HasValue &&
                        d.Tanggal.Value.ToString("dddd", new CultureInfo("id-ID"))
                            .Equals(hari, StringComparison.OrdinalIgnoreCase))
                    .AsQueryable(); // opsional kalau mau chaining
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(d => !string.IsNullOrEmpty(d.NamaKegiatan) &&
                                         d.NamaKegiatan.ToLower().Contains(search.ToLower()));
            }


            // Filter minggu pakai GetWeekOfMonth => harus pakai AsEnumerable()
            if (minggu.HasValue)
            {
                query = query
                    .AsEnumerable()
                    .Where(d => d.Tanggal.HasValue && GetWeekOfMonth(d.Tanggal.Value) == minggu.Value)
                    .AsQueryable(); // opsional jika mau chaining lagi
            }

            var result = query
                .Where(d => d.Tanggal.HasValue)
                .OrderBy(d => d.Tanggal)
                .ThenBy(d => d.JamMulai)
                .ToList();

            ViewBag.SelectedHari = hari;
            ViewBag.SelectedMinggu = minggu;
            ViewBag.SelectedBulan = bulan ?? DateTime.Now.Month;
            ViewBag.Search = search;

            return View(result);
        }

        private int GetWeekOfMonth(DateTime date)
        {
            var first = new DateTime(date.Year, date.Month, 1);
            var firstDayOfWeek = (int)first.DayOfWeek;
            if (firstDayOfWeek == 0) firstDayOfWeek = 7;
            return ((date.Day + firstDayOfWeek - 2) / 7) + 1;
        }

        public IActionResult Create()
        {
            IsiDropdown();
            return View(new JadwalTeknisi());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(JadwalTeknisi jadwal, int[]? SelectedTeknisiIds, int[]? SelectedSupportIds)
        {
            SelectedTeknisiIds ??= Array.Empty<int>();
            SelectedSupportIds ??= Array.Empty<int>();

            if (SelectedSupportIds.Length == 0)
                ModelState.AddModelError("SelectedSupportIds", "Minimal pilih 1 Jenis Support.");

            if (jadwal.JamSelesai <= jadwal.JamMulai)
                ModelState.AddModelError("JamSelesai", "Jam selesai tidak boleh sebelum atau sama dengan jam mulai.");

            if (jadwal.RuangRapatId == 0)
                jadwal.RuangRapatId = null;

            if (ModelState.IsValid)
            {
                foreach (var tekId in SelectedTeknisiIds.Distinct())
                    jadwal.TeknisiJadwals.Add(new TeknisiJadwal { TeknisiId = tekId });

                foreach (var supId in SelectedSupportIds.Distinct())
                    jadwal.JenisSupportJadwals.Add(new JenisSupportJadwal { JenisSupportId = supId });

                _context.Add(jadwal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["SelectedTeknisiIds"] = SelectedTeknisiIds;
            ViewData["SelectedSupportIds"] = SelectedSupportIds;
            IsiDropdown(jadwal);
            return View(jadwal);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var jadwal = await _context.jadwalteknisi
                .Include(j => j.TeknisiJadwals).ThenInclude(tj => tj.Teknisi)
                .Include(j => j.JenisSupportJadwals).ThenInclude(jsj => jsj.JenisSupport)
                .AsNoTracking()
                .FirstOrDefaultAsync(j => j.JadwalTeknisiId == id);

            if (jadwal == null) return NotFound();

            ViewData["SelectedTeknisiIds"] = jadwal.TeknisiJadwals.Select(t => t.TeknisiId).ToArray();
            ViewData["SelectedSupportIds"] = jadwal.JenisSupportJadwals.Select(s => s.JenisSupportId).ToArray();
            IsiDropdown(jadwal, id);
            return View(jadwal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, JadwalTeknisi formModel, int[]? SelectedTeknisiIds, int[]? SelectedSupportIds)
        {
            if (id != formModel.JadwalTeknisiId) return NotFound();

            SelectedTeknisiIds ??= Array.Empty<int>();
            SelectedSupportIds ??= Array.Empty<int>();

            if (SelectedSupportIds.Length == 0)
                ModelState.AddModelError("SelectedSupportIds", "Minimal pilih 1 Jenis Support.");

            if (formModel.JamSelesai <= formModel.JamMulai)
                ModelState.AddModelError("JamSelesai", "Jam selesai tidak boleh sebelum atau sama dengan jam mulai.");

            if (ModelState.IsValid)
            {
                var jadwal = await _context.jadwalteknisi
                    .Include(j => j.TeknisiJadwals)
                    .Include(j => j.JenisSupportJadwals)
                    .FirstOrDefaultAsync(j => j.JadwalTeknisiId == id);

                if (jadwal == null) return NotFound();

                jadwal.NamaKegiatan = formModel.NamaKegiatan;
                jadwal.Tanggal = formModel.Tanggal;
                jadwal.JamMulai = formModel.JamMulai;
                jadwal.JamSelesai = formModel.JamSelesai;
                jadwal.RuangRapatId = formModel.RuangRapatId == 0 ? null : formModel.RuangRapatId;
                jadwal.FungsiId = formModel.FungsiId;
                jadwal.BagianId = formModel.BagianId;
                jadwal.UserNama = formModel.UserNama;
                jadwal.UserKontak = formModel.UserKontak;
                jadwal.Status = formModel.Status;

                SyncCollection(jadwal.TeknisiJadwals, SelectedTeknisiIds, x => x.TeknisiId, idToAdd => new TeknisiJadwal { TeknisiId = idToAdd });
                SyncCollection(jadwal.JenisSupportJadwals, SelectedSupportIds, x => x.JenisSupportId, idToAdd => new JenisSupportJadwal { JenisSupportId = idToAdd });

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["SelectedTeknisiIds"] = SelectedTeknisiIds;
            ViewData["SelectedSupportIds"] = SelectedSupportIds;
            IsiDropdown(formModel, id);
            return View(formModel);
        }

        [HttpGet]
        public IActionResult GetBagianByFungsi(int fungsiId)
        {
            try
            {
                var bagianList = _context.Bagian
                    .AsNoTracking()
                    .Where(b => b.FungsiId == fungsiId)
                    .Select(b => new { id = b.BagianId, nama = b.NamaBagian })
                    .ToList();

                return Json(bagianList);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message, inner = ex.InnerException?.Message });
            }
        }

        private void IsiDropdown(JadwalTeknisi? jadwal = null, int? excludeJadwalId = null)
        {
            var selectedSup = (IEnumerable<int>)(ViewData["SelectedSupportIds"] ?? jadwal?.JenisSupportJadwals?.Select(s => s.JenisSupportId) ?? []);
            Dictionary<int, int> countDict = new();

            if (jadwal?.Tanggal != null)
                countDict = GetTeknisiScheduleCountsByDate(jadwal.Tanggal.Value.Date, excludeJadwalId);

            var teknisiVmList = _context.teknisi.AsNoTracking()
                .Select(t => new TeknisiSelectVm
                {
                    TeknisiId = t.TeknisiId,
                    Nama = t.Nama ?? string.Empty,
                    CountAcaraHariItu = countDict.ContainsKey(t.TeknisiId) ? countDict[t.TeknisiId] : 0
                })
                .OrderBy(t => t.Nama)
                .ToList();

            ViewBag.TeknisiSelectVmList = teknisiVmList;
            ViewBag.RuangRapatId = new SelectList(_context.ruangrapat.AsNoTracking(), "RuangRapatId", "NamaRuang", jadwal?.RuangRapatId);
            ViewBag.JenisSupportList = new MultiSelectList(_context.jenissupport.AsNoTracking(), "JenisSupportId", "NamaJenisSupport", selectedSup);
            ViewBag.FungsiList = new SelectList(_context.Fungsi.AsNoTracking(), "FungsiId", "NamaFungsi", jadwal?.FungsiId);
            ViewBag.StatusList = new SelectList(new[] { "Pending", "Diproses", "Selesai" }, jadwal?.Status);
        }

        private Dictionary<int, int> GetTeknisiScheduleCountsByDate(DateTime tanggal, int? excludeJadwalId = null)
        {
            var query = _context.jadwalteknisi.AsNoTracking().Where(j => j.Tanggal.HasValue && j.Tanggal.Value.Date == tanggal);
            if (excludeJadwalId.HasValue)
                query = query.Where(j => j.JadwalTeknisiId != excludeJadwalId);

            return query.SelectMany(j => j.TeknisiJadwals)
                .GroupBy(tj => tj.TeknisiId)
                .Select(g => new { TeknisiId = g.Key, Count = g.Count() })
                .ToDictionary(x => x.TeknisiId, x => x.Count);
        }

        private static void SyncCollection<TJoin>(ICollection<TJoin> existingCollection, IEnumerable<int> newIds, Func<TJoin, int> existingIdSelector, Func<int, TJoin> createJoinEntity)
        {
            var newIdSet = new HashSet<int>(newIds);
            var toRemove = existingCollection.Where(e => !newIdSet.Contains(existingIdSelector(e))).ToList();
            foreach (var item in toRemove)
                existingCollection.Remove(item);

            var existingIdSet = existingCollection.Select(existingIdSelector).ToHashSet();
            foreach (var id in newIdSet)
            {
                if (!existingIdSet.Contains(id))
                    existingCollection.Add(createJoinEntity(id));
            }
        }
    }
}
