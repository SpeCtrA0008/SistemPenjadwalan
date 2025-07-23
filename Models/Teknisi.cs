using System.ComponentModel.DataAnnotations;

namespace AdminDashboardApp.Models
{
    public class Teknisi
    {
        [Key]
        public int TeknisiId { get; set; }
        public string?Nama { get; set; }
        public string?Skill { get; set; }
        public string? RuangRapatUtama { get; set; }
        public ICollection<TeknisiJadwal> TeknisiJadwals { get; set; } = new List<TeknisiJadwal>();


    }
}
