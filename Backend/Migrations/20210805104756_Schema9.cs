using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tasinmaz_v3.Migrations
{
    public partial class Schema9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "passwordHash",
                table: "kullanicilar",
                type: "text",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "bytea",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "passwordHash",
                table: "kullanicilar",
                type: "bytea",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
