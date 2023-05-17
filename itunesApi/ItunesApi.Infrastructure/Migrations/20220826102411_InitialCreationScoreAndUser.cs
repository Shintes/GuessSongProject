using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItunesApi.Infrastructure.Migrations
{
    public partial class InitialCreationScoreAndUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Logins",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Scores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Points = table.Column<int>(type: "int", maxLength: 255, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", maxLength: 255, nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Scores_Logins_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Logins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Logins_AccountId",
                table: "Logins",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Scores_AccountId",
                table: "Scores",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Logins_Logins_AccountId",
                table: "Logins",
                column: "AccountId",
                principalTable: "Logins",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logins_Logins_AccountId",
                table: "Logins");

            migrationBuilder.DropTable(
                name: "Scores");

            migrationBuilder.DropIndex(
                name: "IX_Logins_AccountId",
                table: "Logins");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Logins");
        }
    }
}
