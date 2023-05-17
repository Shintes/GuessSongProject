using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItunesApi.Infrastructure.Migrations
{
    public partial class RedoPasswordSystemLogin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Logins");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Logins",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Logins",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Logins");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Logins");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Logins",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
