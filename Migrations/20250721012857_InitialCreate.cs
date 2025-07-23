using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminDashboardApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Fungsi",
                columns: table => new
                {
                    fungsi_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nama_fungsi = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fungsi", x => x.fungsi_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "jenissupport",
                columns: table => new
                {
                    JenisSupportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    jenissupport = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Keterangan = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_jenissupport", x => x.JenisSupportId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ruangrapat",
                columns: table => new
                {
                    RuangRapatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NamaGedung = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Lantai = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NamaRuang = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ruangrapat", x => x.RuangRapatId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "teknisi",
                columns: table => new
                {
                    TeknisiId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nama = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Skill = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RuangRapatUtama = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teknisi", x => x.TeknisiId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Bagian",
                columns: table => new
                {
                    BagianId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NamaBagian = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FungsiId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bagian", x => x.BagianId);
                    table.ForeignKey(
                        name: "FK_Bagian_Fungsi_FungsiId",
                        column: x => x.FungsiId,
                        principalTable: "Fungsi",
                        principalColumn: "fungsi_id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "jadwalteknisi",
                columns: table => new
                {
                    JadwalTeknisiId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NamaKegiatan = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tanggal = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    JamMulai = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    JamSelesai = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    RuangRapatId = table.Column<int>(type: "int", nullable: true),
                    FungsiId = table.Column<int>(type: "int", nullable: true),
                    BagianId = table.Column<int>(type: "int", nullable: true),
                    UserNama = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserKontak = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    JenisSupportId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_jadwalteknisi", x => x.JadwalTeknisiId);
                    table.ForeignKey(
                        name: "FK_jadwalteknisi_Bagian_BagianId",
                        column: x => x.BagianId,
                        principalTable: "Bagian",
                        principalColumn: "BagianId");
                    table.ForeignKey(
                        name: "FK_jadwalteknisi_Fungsi_FungsiId",
                        column: x => x.FungsiId,
                        principalTable: "Fungsi",
                        principalColumn: "fungsi_id");
                    table.ForeignKey(
                        name: "FK_jadwalteknisi_jenissupport_JenisSupportId",
                        column: x => x.JenisSupportId,
                        principalTable: "jenissupport",
                        principalColumn: "JenisSupportId");
                    table.ForeignKey(
                        name: "FK_jadwalteknisi_ruangrapat_RuangRapatId",
                        column: x => x.RuangRapatId,
                        principalTable: "ruangrapat",
                        principalColumn: "RuangRapatId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "peminjamanalat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NamaAlat = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tanggal = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    BagianId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_peminjamanalat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_peminjamanalat_Bagian_BagianId",
                        column: x => x.BagianId,
                        principalTable: "Bagian",
                        principalColumn: "BagianId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "JenisSupportJadwal",
                columns: table => new
                {
                    JenisSupportId = table.Column<int>(type: "int", nullable: false),
                    JadwalTeknisiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JenisSupportJadwal", x => new { x.JenisSupportId, x.JadwalTeknisiId });
                    table.ForeignKey(
                        name: "FK_JenisSupportJadwal_jadwalteknisi_JadwalTeknisiId",
                        column: x => x.JadwalTeknisiId,
                        principalTable: "jadwalteknisi",
                        principalColumn: "JadwalTeknisiId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JenisSupportJadwal_jenissupport_JenisSupportId",
                        column: x => x.JenisSupportId,
                        principalTable: "jenissupport",
                        principalColumn: "JenisSupportId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TeknisiJadwal",
                columns: table => new
                {
                    TeknisiId = table.Column<int>(type: "int", nullable: false),
                    JadwalTeknisiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeknisiJadwal", x => new { x.TeknisiId, x.JadwalTeknisiId });
                    table.ForeignKey(
                        name: "FK_TeknisiJadwal_jadwalteknisi_JadwalTeknisiId",
                        column: x => x.JadwalTeknisiId,
                        principalTable: "jadwalteknisi",
                        principalColumn: "JadwalTeknisiId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeknisiJadwal_teknisi_TeknisiId",
                        column: x => x.TeknisiId,
                        principalTable: "teknisi",
                        principalColumn: "TeknisiId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Bagian_FungsiId",
                table: "Bagian",
                column: "FungsiId");

            migrationBuilder.CreateIndex(
                name: "IX_jadwalteknisi_BagianId",
                table: "jadwalteknisi",
                column: "BagianId");

            migrationBuilder.CreateIndex(
                name: "IX_jadwalteknisi_FungsiId",
                table: "jadwalteknisi",
                column: "FungsiId");

            migrationBuilder.CreateIndex(
                name: "IX_jadwalteknisi_JenisSupportId",
                table: "jadwalteknisi",
                column: "JenisSupportId");

            migrationBuilder.CreateIndex(
                name: "IX_jadwalteknisi_RuangRapatId",
                table: "jadwalteknisi",
                column: "RuangRapatId");

            migrationBuilder.CreateIndex(
                name: "IX_JenisSupportJadwal_JadwalTeknisiId",
                table: "JenisSupportJadwal",
                column: "JadwalTeknisiId");

            migrationBuilder.CreateIndex(
                name: "IX_peminjamanalat_BagianId",
                table: "peminjamanalat",
                column: "BagianId");

            migrationBuilder.CreateIndex(
                name: "IX_TeknisiJadwal_JadwalTeknisiId",
                table: "TeknisiJadwal",
                column: "JadwalTeknisiId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JenisSupportJadwal");

            migrationBuilder.DropTable(
                name: "peminjamanalat");

            migrationBuilder.DropTable(
                name: "TeknisiJadwal");

            migrationBuilder.DropTable(
                name: "jadwalteknisi");

            migrationBuilder.DropTable(
                name: "teknisi");

            migrationBuilder.DropTable(
                name: "Bagian");

            migrationBuilder.DropTable(
                name: "jenissupport");

            migrationBuilder.DropTable(
                name: "ruangrapat");

            migrationBuilder.DropTable(
                name: "Fungsi");
        }
    }
}
