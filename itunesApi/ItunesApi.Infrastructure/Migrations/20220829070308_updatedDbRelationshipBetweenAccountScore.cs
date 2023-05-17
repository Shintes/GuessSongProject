using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItunesApi.Infrastructure.Migrations
{
    public partial class updatedDbRelationshipBetweenAccountScore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logins_Logins_AccountId",
                table: "Logins");

            migrationBuilder.DropIndex(
                name: "IX_Logins_AccountId",
                table: "Logins");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Logins");

            migrationBuilder.CreateIndex(
                name: "IX_Logins_Email",
                table: "Logins",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Logins_Email",
                table: "Logins");

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Logins",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Logins_AccountId",
                table: "Logins",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Logins_Logins_AccountId",
                table: "Logins",
                column: "AccountId",
                principalTable: "Logins",
                principalColumn: "Id");
        }
    }
}
