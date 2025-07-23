using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AdminDashboardApp.Models;

public class Fungsi
{
    [Key]
    [Column("fungsi_id")] // ⬅ Tambahkan ini
    public int? FungsiId { get; set; }

    [Column("nama_fungsi")] // ⬅ jika kolom di database pakai nama ini
    public string? NamaFungsi { get; set; }

    public ICollection<Bagian> Bagians { get; set; } = new List<Bagian>();
}
