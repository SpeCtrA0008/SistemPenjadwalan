namespace AdminDashboardApp.Models
{
    public class JenisSupportJadwal
    {
        public int JenisSupportId { get; set; }
        public JenisSupport JenisSupport { get; set; } = null!;

        public int JadwalTeknisiId { get; set; }
        public JadwalTeknisi JadwalTeknisi { get; set; } = null!;
    }
}
