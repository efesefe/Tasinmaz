using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tasinmaz_v3.Migrations
{
    public partial class Schema8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "passwordSalt",
                table: "kullanicilar");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "passwordSalt",
                table: "kullanicilar",
                type: "bytea",
                nullable: true);
        }
    }
}
