﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RendszerRepo.Migrations
{
    /// <inheritdoc />
    public partial class Project : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Parts",
                columns: table => new
                {
                    partId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    partName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<int>(type: "int", nullable: false),
                    maxCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parts", x => x.partId);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.ProjectId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userRole = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "Storages",
                columns: table => new
                {
                    storageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    partId = table.Column<int>(type: "int", nullable: false),
                    row = table.Column<int>(type: "int", nullable: false),
                    column = table.Column<int>(type: "int", nullable: false),
                    drawer = table.Column<int>(type: "int", nullable: false),
                    countOfParts = table.Column<int>(type: "int", nullable: false),
                    max = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Storages", x => x.storageId);
                    table.ForeignKey(
                        name: "FK_Storages_Parts_partId",
                        column: x => x.partId,
                        principalTable: "Parts",
                        principalColumn: "partId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectProperties",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    assignedId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    partId = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    combinedPrice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_ProjectProperties_Parts_partId",
                        column: x => x.partId,
                        principalTable: "Parts",
                        principalColumn: "partId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectProperties_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectProperties_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProperties_partId",
                table: "ProjectProperties",
                column: "partId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProperties_ProjectId",
                table: "ProjectProperties",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProperties_UserId",
                table: "ProjectProperties",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Storages_partId",
                table: "Storages",
                column: "partId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectProperties");

            migrationBuilder.DropTable(
                name: "Storages");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Parts");
        }
    }
}
