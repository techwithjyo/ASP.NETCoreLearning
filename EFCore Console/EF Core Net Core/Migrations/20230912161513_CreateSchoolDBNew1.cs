using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EF_Core_Net_Core.Migrations
{
    /// <inheritdoc />
    public partial class CreateSchoolDBNew1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SomeRandomStudentString",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SomeRandomStudentString",
                table: "Students");
        }
    }
}
