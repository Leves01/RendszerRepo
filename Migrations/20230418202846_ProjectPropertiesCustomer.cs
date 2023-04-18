using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RendszerRepo.Migrations
{
    /// <inheritdoc />
    public partial class ProjectPropertiesCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "ProjectProperties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "ProjectProperties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "ProjectProperties");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "ProjectProperties");
        }
    }
}
