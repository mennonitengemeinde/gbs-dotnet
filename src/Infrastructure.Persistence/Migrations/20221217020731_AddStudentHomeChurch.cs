using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gbs.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentHomeChurch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HomeChurch",
                table: "Students",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HomeChurch",
                table: "Students");
        }
    }
}
