using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RendszerRepo.Migrations
{
    /// <inheritdoc />
    public partial class Reserves : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reserves",
                columns: table => new
                {
                    reservedPartsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    partId = table.Column<int>(type: "int", nullable: false),
                    projectId = table.Column<int>(type: "int", nullable: false),
                    neededAmount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserves", x => x.reservedPartsId);
                    table.ForeignKey(
                        name: "FK_Reserves_Parts_partId",
                        column: x => x.partId,
                        principalTable: "Parts",
                        principalColumn: "partId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reserves_Project_projectId",
                        column: x => x.projectId,
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reserves_partId",
                table: "Reserves",
                column: "partId");

            migrationBuilder.CreateIndex(
                name: "IX_Reserves_projectId",
                table: "Reserves",
                column: "projectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reserves");
        }
    }
}
