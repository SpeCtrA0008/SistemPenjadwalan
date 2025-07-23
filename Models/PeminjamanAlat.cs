using System;
using System.ComponentModel.DataAnnotations.Schema;
using AdminDashboardApp.Models;


public class PeminjamanAlat
{
    public int Id { get; set; }
    public string? NamaAlat { get; set; }
    public DateTime? Tanggal { get; set; }

    // Relasi ke Bagian
    public int? BagianId { get; set; }

    [ForeignKey("BagianId")]
    public Bagian? Bagian { get; set; }
}
