using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Grading_System_Backend.Migrations
{
    /// <inheritdoc />
    public partial class addGradeAndTotal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Grade",
                table: "StudentSubjects",
                type: "nvarchar(1)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "Total",
                table: "StudentSubjects",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grade",
                table: "StudentSubjects");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "StudentSubjects");
        }
    }
}
