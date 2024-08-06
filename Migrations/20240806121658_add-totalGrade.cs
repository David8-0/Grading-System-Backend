using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Grading_System_Backend.Migrations
{
    /// <inheritdoc />
    public partial class addtotalGrade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TotalGrade",
                table: "Students",
                type: "nvarchar(1)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalGrade",
                table: "Students");
        }
    }
}
