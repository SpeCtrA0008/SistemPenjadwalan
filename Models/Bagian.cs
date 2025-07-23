using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminDashboardApp.Models
{
    public class Bagian
    {
        [Key]
        public int BagianId { get; set; }

        public string NamaBagian { get; set; }

        public int? FungsiId { get; set; }
        [ForeignKey("FungsiId")]
        public Fungsi? Fungsi { get; set; }
        
    }
}
