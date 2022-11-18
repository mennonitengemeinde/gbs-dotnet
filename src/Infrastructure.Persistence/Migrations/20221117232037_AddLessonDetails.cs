using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gbs.Infrastructure.Persistence.Migrations
{
    public partial class AddLessonDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GenerationId",
                table: "Lessons",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IsVisible",
                table: "Lessons",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Lessons",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "VideoUrl",
                table: "Lessons",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_GenerationId",
                table: "Lessons",
                column: "GenerationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Generations_GenerationId",
                table: "Lessons",
                column: "GenerationId",
                principalTable: "Generations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Generations_GenerationId",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_GenerationId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "GenerationId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "VideoUrl",
                table: "Lessons");
        }
    }
}
