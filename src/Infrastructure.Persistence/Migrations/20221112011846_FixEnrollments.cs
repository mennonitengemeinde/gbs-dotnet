using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Gbs.Infrastructure.Persistence.Migrations
{
    public partial class FixEnrollments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Enrollments_EnrollmentStudentId_EnrollmentGeneration~",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Grades_EnrollmentStudentId_EnrollmentGenerationId",
                table: "Grades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "EnrollmentGenerationId",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "EnrollmentStudentId",
                table: "Grades");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CompletionDate",
                table: "Enrollments",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Enrollments",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_EnrollmentId",
                table: "Grades",
                column: "EnrollmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_StudentId",
                table: "Enrollments",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Enrollments_EnrollmentId",
                table: "Grades",
                column: "EnrollmentId",
                principalTable: "Enrollments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Subjects_SubjectId",
                table: "Grades",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Enrollments_EnrollmentId",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Subjects_SubjectId",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Grades_EnrollmentId",
                table: "Grades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_StudentId",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Enrollments");

            migrationBuilder.AddColumn<int>(
                name: "EnrollmentGenerationId",
                table: "Grades",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EnrollmentStudentId",
                table: "Grades",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CompletionDate",
                table: "Enrollments",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enrollments",
                table: "Enrollments",
                columns: new[] { "StudentId", "GenerationId" });

            migrationBuilder.CreateIndex(
                name: "IX_Grades_EnrollmentStudentId_EnrollmentGenerationId",
                table: "Grades",
                columns: new[] { "EnrollmentStudentId", "EnrollmentGenerationId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Enrollments_EnrollmentStudentId_EnrollmentGeneration~",
                table: "Grades",
                columns: new[] { "EnrollmentStudentId", "EnrollmentGenerationId" },
                principalTable: "Enrollments",
                principalColumns: new[] { "StudentId", "GenerationId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
