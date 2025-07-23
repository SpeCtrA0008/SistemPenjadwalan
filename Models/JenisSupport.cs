using AdminDashboardApp.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class JenisSupport
{
    [Key]
    public int JenisSupportId { get; set; }

    [Required(ErrorMessage = "Jenis Support wajib diisi")]
    [Display(Name = "Jenis Support")]
    [Column("jenissupport")] // mapping ke kolom DB yang bernama 'JenisSupport'
    public string NamaJenisSupport { get; set; } = string.Empty;

    [Display(Name = "Keterangan")]
    public string? Keterangan { get; set; }

    public ICollection<JadwalTeknisi> JadwalTeknisis { get; set; } = new List<JadwalTeknisi>();
    public ICollection<JenisSupportJadwal> JenisSupportJadwals { get; set; } = new List<JenisSupportJadwal>();


}
