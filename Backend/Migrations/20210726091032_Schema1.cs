using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace tasinmaz_v3.Migrations
{
    public partial class Schema1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "iller",
                columns: table => new
                {
                    ilID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ilName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_iller", x => x.ilID);
                });

            migrationBuilder.CreateTable(
                name: "kullanicilar",
                columns: table => new
                {
                    kullaniciID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    isim = table.Column<string>(type: "text", nullable: true),
                    soyisim = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    password = table.Column<string>(type: "text", nullable: true),
                    role = table.Column<bool>(type: "boolean", nullable: false),
                    isActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kullanicilar", x => x.kullaniciID);
                });

            migrationBuilder.CreateTable(
                name: "loglar",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    kullaniciId = table.Column<int>(type: "integer", nullable: false),
                    durum = table.Column<bool>(type: "boolean", nullable: false),
                    islemTip = table.Column<int>(type: "integer", nullable: false),
                    aciklama = table.Column<string>(type: "text", nullable: true),
                    tarihSaat = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ip = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_loglar", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ilceler",
                columns: table => new
                {
                    ilceID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ilceName = table.Column<string>(type: "text", nullable: true),
                    ilID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ilceler", x => x.ilceID);
                    table.ForeignKey(
                        name: "FK_ilceler_iller_ilID",
                        column: x => x.ilID,
                        principalTable: "iller",
                        principalColumn: "ilID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mahalleler",
                columns: table => new
                {
                    mahalleID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    mahalleName = table.Column<string>(type: "text", nullable: true),
                    ilceID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mahalleler", x => x.mahalleID);
                    table.ForeignKey(
                        name: "FK_mahalleler_ilceler_ilceID",
                        column: x => x.ilceID,
                        principalTable: "ilceler",
                        principalColumn: "ilceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tasinmazlar",
                columns: table => new
                {
                    tasinmazID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    mahalleID = table.Column<int>(type: "integer", nullable: false),
                    Ada = table.Column<int>(type: "integer", nullable: false),
                    Parsel = table.Column<int>(type: "integer", nullable: false),
                    nitelik = table.Column<string>(type: "text", nullable: true),
                    adres = table.Column<string>(type: "text", nullable: true),
                    isActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tasinmazlar", x => x.tasinmazID);
                    table.ForeignKey(
                        name: "FK_tasinmazlar_mahalleler_mahalleID",
                        column: x => x.mahalleID,
                        principalTable: "mahalleler",
                        principalColumn: "mahalleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ilceler_ilID",
                table: "ilceler",
                column: "ilID");

            migrationBuilder.CreateIndex(
                name: "IX_mahalleler_ilceID",
                table: "mahalleler",
                column: "ilceID");

            migrationBuilder.CreateIndex(
                name: "IX_tasinmazlar_mahalleID",
                table: "tasinmazlar",
                column: "mahalleID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "kullanicilar");

            migrationBuilder.DropTable(
                name: "loglar");

            migrationBuilder.DropTable(
                name: "tasinmazlar");

            migrationBuilder.DropTable(
                name: "mahalleler");

            migrationBuilder.DropTable(
                name: "ilceler");

            migrationBuilder.DropTable(
                name: "iller");
        }
    }
}
