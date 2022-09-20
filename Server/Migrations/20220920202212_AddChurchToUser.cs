using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gbs.Server.Migrations
{
    public partial class AddChurchToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChurchId",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ChurchId",
                table: "Users",
                column: "ChurchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Churches_ChurchId",
                table: "Users",
                column: "ChurchId",
                principalTable: "Churches",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Churches_ChurchId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ChurchId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ChurchId",
                table: "Users");
        }
    }
}
