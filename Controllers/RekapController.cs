using AdminDashboardApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace AdminDashboardApp.Controllers
{
    public class RekapController : Controller
    {
        private readonly AppDbContext _context;

        public RekapController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? year, int? fungsiId)
        {
            var selectedYear = year ?? DateTime.Now.Year;
            var currentMonth = DateTime.Now.Month;

            // Jumlah jadwal teknisi per bulan
            var bulanList = Enumerable.Range(1, 12)
                .Select(b => new RekapPeminjamanViewModel
                {
                    Bulan = CultureInfo.GetCultureInfo("id-ID").DateTimeFormat.GetMonthName(b),
                    JumlahPeminjaman = _context.jadwalteknisi
                        .Count(j => j.Tanggal.HasValue &&
                                    j.Tanggal.Value.Month == b &&
                                    j.Tanggal.Value.Year == selectedYear)
                })
                .ToList();

            // Grafik Teknisi bulan ini
            var grafikTeknisi = _context.jadwalteknisi
                .Include(j => j.TeknisiJadwals)
                    .ThenInclude(tj => tj.Teknisi)
                .AsNoTracking()
                .Where(j => j.Tanggal.HasValue &&
                            j.Tanggal.Value.Month == currentMonth &&
                            j.Tanggal.Value.Year == selectedYear)
                .SelectMany(j => j.TeknisiJadwals.Select(tj => tj.Teknisi.Nama))
                .GroupBy(nama => nama ?? "Tidak Diketahui")
                .Select(g => new GrafikTeknisiViewModel
                {
                    NamaTeknisi = g.Key,
                    JumlahJadwal = g.Count()
                })
                .OrderByDescending(g => g.JumlahJadwal)
                .ToList();

            // Grafik peminjaman alat per bagian
            var jadwalQuery = _context.jadwalteknisi
                .Include(j => j.Bagian)
                    .ThenInclude(b => b.Fungsi)
                .Where(j => j.Tanggal.HasValue &&
                            j.Tanggal.Value.Month == currentMonth &&
                            j.Tanggal.Value.Year == selectedYear &&
                            j.BagianId != null);

            if (fungsiId.HasValue)
            {
                jadwalQuery = jadwalQuery.Where(j => j.Bagian != null && j.Bagian.FungsiId == fungsiId.Value);
            }

            var rekapPeminjaman = jadwalQuery
                .Where(j => j.Bagian != null)
                .GroupBy(j => j.Bagian.NamaBagian)
                .Select(g => new PeminjamanPerBagianViewModel
                {
                    NamaBagian = g.Key,
                    JumlahPeminjaman = g.Count()
                })
                .ToList();

            // ViewModel lengkap
            var viewModel = new RekapViewModel
            {
                RekapPeminjamanPerBulan = bulanList,
                RekapJadwalTeknisi = grafikTeknisi,
                RekapPeminjamanPerBagian = rekapPeminjaman,
                FungsiList = _context.Fungsi.ToList(),
                SelectedFungsiId = fungsiId
            };

            return View(viewModel);
        }
    }
}
