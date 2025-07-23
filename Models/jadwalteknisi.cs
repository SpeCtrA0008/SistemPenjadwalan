using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminDashboardApp.Models
{
    public class JadwalTeknisi
    {
        [Key]
        public int JadwalTeknisiId { get; set; }

        [Required(ErrorMessage = "Nama kegiatan wajib diisi")]
        public string NamaKegiatan { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tanggal wajib diisi")]
        public DateTime? Tanggal { get; set; }

        [Required(ErrorMessage = "Jam Mulai wajib diisi")]
        public TimeSpan? JamMulai { get; set; }

        [Required(ErrorMessage = "Jam Selesai wajib diisi")]
        public TimeSpan? JamSelesai { get; set; }

        public int? RuangRapatId { get; set; }

        [Column("FungsiId")]
        public int? FungsiId { get; set; }
        public Fungsi? Fungsi { get; set; }

        [Column("BagianId")]
        public int? BagianId { get; set; }
        [ForeignKey("BagianId")]
        public Bagian? Bagian { get; set; }

        [Required(ErrorMessage = "Nama user wajib diisi")]
        public string UserNama { get; set; } = string.Empty;

        [Required(ErrorMessage = "Kontak user wajib diisi")]
        public string UserKontak { get; set; } = string.Empty;

        [Required(ErrorMessage = "Status wajib diisi")]
        public string Status { get; set; } = string.Empty;

        [ForeignKey("RuangRapatId")]
        public RuangRapat? RuangRapat { get; set; }

        // ✅ Hapus JenisSupportId karena kita pakai relasi many-to-many
        

        // ✅ Relasi many-to-many: 1 jadwal bisa punya banyak teknisi
        public ICollection<TeknisiJadwal> TeknisiJadwals { get; set; } = new List<TeknisiJadwal>();

        // ✅ Relasi many-to-many untuk Jenis Support
        public ICollection<JenisSupportJadwal> JenisSupportJadwals { get; set; } = new List<JenisSupportJadwal>();
    }
}
