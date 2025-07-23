using System.ComponentModel.DataAnnotations.Schema;

namespace AdminDashboardApp.Models
{
    public class TeknisiJadwal
    {
        // Foreign key ke tabel Teknisi
        public int TeknisiId { get; set; }
        public Teknisi? Teknisi { get; set; }

        // Foreign key ke tabel JadwalTeknisi
        public int JadwalTeknisiId { get; set; }
        public JadwalTeknisi? JadwalTeknisi { get; set; }
    }
}
