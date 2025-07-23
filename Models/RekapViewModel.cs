using System.Collections.Generic;

namespace AdminDashboardApp.Models
{
    public class RekapViewModel
    {
        public string Bulan { get; set; } = "";
        public List<RekapPeminjamanViewModel> RekapPeminjamanPerBulan { get; set; } = new();
        public List<GrafikTeknisiViewModel> RekapJadwalTeknisi { get; set; } = new();
        public List<PeminjamanPerBagianViewModel> RekapPeminjamanPerBagian { get; set; }
        public List<Fungsi> FungsiList { get; set; }
        public int? SelectedFungsiId { get; set; }
    }
}
