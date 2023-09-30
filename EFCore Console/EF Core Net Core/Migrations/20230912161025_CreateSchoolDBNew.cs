using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EF_Core_Net_Core.Migrations
{
    /// <inheritdoc />
    public partial class CreateSchoolDBNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RandomString",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RandomString",
                table: "Courses");
        }
    }
}
