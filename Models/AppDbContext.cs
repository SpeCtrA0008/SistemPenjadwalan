using Microsoft.EntityFrameworkCore;

namespace AdminDashboardApp.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<JadwalTeknisi> jadwalteknisi { get; set; }
        public DbSet<Teknisi> teknisi { get; set; }
        public DbSet<RuangRapat> ruangrapat { get; set; }
        public DbSet<JenisSupport> jenissupport { get; set; }
        public DbSet<Fungsi> Fungsi { get; set; }
        public DbSet<Bagian> Bagian { get; set; }
        public DbSet<PeminjamanAlat> peminjamanalat { get; set; }

        public DbSet<TeknisiJadwal> TeknisiJadwal { get; set; }
        public DbSet<JenisSupportJadwal> JenisSupportJadwal { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ===========================
            // Relasi many-to-many: Teknisi <-> JadwalTeknisi
            // ===========================
            modelBuilder.Entity<TeknisiJadwal>()
                .HasKey(tj => new { tj.TeknisiId, tj.JadwalTeknisiId });

            modelBuilder.Entity<TeknisiJadwal>()
                .HasOne(tj => tj.Teknisi)
                .WithMany(t => t.TeknisiJadwals)
                .HasForeignKey(tj => tj.TeknisiId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TeknisiJadwal>()
                .HasOne(tj => tj.JadwalTeknisi)
                .WithMany(j => j.TeknisiJadwals)
                .HasForeignKey(tj => tj.JadwalTeknisiId)
                .OnDelete(DeleteBehavior.Cascade);

            // ===========================
            // Relasi many-to-many: JenisSupport <-> JadwalTeknisi
            // ===========================
            modelBuilder.Entity<JenisSupportJadwal>()
                .HasKey(js => new { js.JenisSupportId, js.JadwalTeknisiId });

            modelBuilder.Entity<JenisSupportJadwal>()
                .HasOne(js => js.JenisSupport)
                .WithMany(j => j.JenisSupportJadwals)
                .HasForeignKey(js => js.JenisSupportId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<JenisSupportJadwal>()
                .HasOne(js => js.JadwalTeknisi)
                .WithMany(j => j.JenisSupportJadwals)
                .HasForeignKey(js => js.JadwalTeknisiId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
