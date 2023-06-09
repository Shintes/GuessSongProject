﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItunesApi.Infrastructure.Migrations
{
    public partial class InitialEmailAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Logins",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Logins");
        }
    }
}
