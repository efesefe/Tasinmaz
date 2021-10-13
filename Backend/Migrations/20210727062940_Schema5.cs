using Microsoft.EntityFrameworkCore.Migrations;

namespace tasinmaz_v3.Migrations
{
    public partial class Schema5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tasinmazlar_mahalleler_mahalleID",
                table: "tasinmazlar");

            migrationBuilder.DropIndex(
                name: "IX_tasinmazlar_mahalleID",
                table: "tasinmazlar");

            migrationBuilder.AddColumn<int>(
                name: "MahalleID",
                table: "tasinmazlar",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tasinmazlar_MahalleID",
                table: "tasinmazlar",
                column: "MahalleID");

            migrationBuilder.AddForeignKey(
                name: "FK_tasinmazlar_mahalleler_MahalleID",
                table: "tasinmazlar",
                column: "MahalleID",
                principalTable: "mahalleler",
                principalColumn: "mahalleID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tasinmazlar_mahalleler_MahalleID",
                table: "tasinmazlar");

            migrationBuilder.DropIndex(
                name: "IX_tasinmazlar_MahalleID",
                table: "tasinmazlar");

            migrationBuilder.DropColumn(
                name: "MahalleID",
                table: "tasinmazlar");

            migrationBuilder.CreateIndex(
                name: "IX_tasinmazlar_mahalleID",
                table: "tasinmazlar",
                column: "mahalleID");

            migrationBuilder.AddForeignKey(
                name: "FK_tasinmazlar_mahalleler_mahalleID",
                table: "tasinmazlar",
                column: "mahalleID",
                principalTable: "mahalleler",
                principalColumn: "mahalleID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
