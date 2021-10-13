using Microsoft.EntityFrameworkCore.Migrations;

namespace tasinmaz_v3.Migrations
{
    public partial class Schema3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_loglar_kullanicilar_kullaniciID",
                table: "loglar");

            migrationBuilder.DropIndex(
                name: "IX_loglar_kullaniciID",
                table: "loglar");

            migrationBuilder.DropColumn(
                name: "kullaniciID",
                table: "loglar");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "kullaniciID",
                table: "loglar",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_loglar_kullaniciID",
                table: "loglar",
                column: "kullaniciID");

            migrationBuilder.AddForeignKey(
                name: "FK_loglar_kullanicilar_kullaniciID",
                table: "loglar",
                column: "kullaniciID",
                principalTable: "kullanicilar",
                principalColumn: "kullaniciID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
