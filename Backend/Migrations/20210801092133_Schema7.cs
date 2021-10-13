using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tasinmaz_v3.Migrations
{
    public partial class Schema7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "password",
                table: "kullanicilar");

            migrationBuilder.AddColumn<byte[]>(
                name: "passwordHash",
                table: "kullanicilar",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "passwordSalt",
                table: "kullanicilar",
                type: "bytea",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "passwordHash",
                table: "kullanicilar");

            migrationBuilder.DropColumn(
                name: "passwordSalt",
                table: "kullanicilar");

            migrationBuilder.AddColumn<string>(
                name: "password",
                table: "kullanicilar",
                type: "text",
                nullable: true);
        }
    }
}
