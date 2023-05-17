using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItunesApi.Infrastructure.Migrations
{
    public partial class AddedUniqueUserName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Logins_UserName",
                table: "Logins",
                column: "UserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Logins_UserName",
                table: "Logins");
        }
    }
}
