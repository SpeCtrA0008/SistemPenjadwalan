using System.ComponentModel.DataAnnotations;

namespace AdminDashboardApp.Models
{
    public class RuangRapat
    {
        [Key]
        public int? RuangRapatId { get; set; }
        public string? NamaGedung { get; set; }
        public string? Lantai { get; set; }
        public string? NamaRuang { get; set; }
    }
}
