using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gbs.Server.Migrations
{
    public partial class AddChurchCountry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Churches",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Churches");
        }
    }
}
