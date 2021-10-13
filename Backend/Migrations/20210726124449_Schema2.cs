using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace tasinmaz_v3.Migrations
{
    public partial class Schema2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "durum",
                table: "loglar");

            migrationBuilder.RenameColumn(
                name: "kullaniciId",
                table: "loglar",
                newName: "kullaniciID");

            migrationBuilder.RenameColumn(
                name: "islemTip",
                table: "loglar",
                newName: "islemID");

            migrationBuilder.AddColumn<int>(
                name: "durumID",
                table: "loglar",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Durumlar",
                columns: table => new
                {
                    durumID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    durumAdi = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Durumlar", x => x.durumID);
                });

            migrationBuilder.CreateTable(
                name: "Islemler",
                columns: table => new
                {
                    islemID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    islemAdi = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Islemler", x => x.islemID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_loglar_durumID",
                table: "loglar",
                column: "durumID");

            migrationBuilder.CreateIndex(
                name: "IX_loglar_islemID",
                table: "loglar",
                column: "islemID");

            migrationBuilder.CreateIndex(
                name: "IX_loglar_kullaniciID",
                table: "loglar",
                column: "kullaniciID");

            migrationBuilder.AddForeignKey(
                name: "FK_loglar_Durumlar_durumID",
                table: "loglar",
                column: "durumID",
                principalTable: "Durumlar",
                principalColumn: "durumID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_loglar_Islemler_islemID",
                table: "loglar",
                column: "islemID",
                principalTable: "Islemler",
                principalColumn: "islemID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_loglar_kullanicilar_kullaniciID",
                table: "loglar",
                column: "kullaniciID",
                principalTable: "kullanicilar",
                principalColumn: "kullaniciID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_loglar_Durumlar_durumID",
                table: "loglar");

            migrationBuilder.DropForeignKey(
                name: "FK_loglar_Islemler_islemID",
                table: "loglar");

            migrationBuilder.DropForeignKey(
                name: "FK_loglar_kullanicilar_kullaniciID",
                table: "loglar");

            migrationBuilder.DropTable(
                name: "Durumlar");

            migrationBuilder.DropTable(
                name: "Islemler");

            migrationBuilder.DropIndex(
                name: "IX_loglar_durumID",
                table: "loglar");

            migrationBuilder.DropIndex(
                name: "IX_loglar_islemID",
                table: "loglar");

            migrationBuilder.DropIndex(
                name: "IX_loglar_kullaniciID",
                table: "loglar");

            migrationBuilder.DropColumn(
                name: "durumID",
                table: "loglar");

            migrationBuilder.RenameColumn(
                name: "kullaniciID",
                table: "loglar",
                newName: "kullaniciId");

            migrationBuilder.RenameColumn(
                name: "islemID",
                table: "loglar",
                newName: "islemTip");

            migrationBuilder.AddColumn<bool>(
                name: "durum",
                table: "loglar",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
