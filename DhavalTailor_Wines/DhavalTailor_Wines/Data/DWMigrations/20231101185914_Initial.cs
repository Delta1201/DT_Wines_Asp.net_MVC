using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DhavalTailor_Wines.Data.DWMigrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Wine_Types",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WineTypeName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wine_Types", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Wines",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WineName = table.Column<string>(type: "TEXT", maxLength: 70, nullable: false),
                    WineYear = table.Column<string>(type: "TEXT", nullable: false),
                    WinePrice = table.Column<double>(type: "REAL", nullable: false),
                    WineHarvest = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Wine_TypeID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wines", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Wines_Wine_Types_Wine_TypeID",
                        column: x => x.Wine_TypeID,
                        principalTable: "Wine_Types",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wine_Types_WineTypeName",
                table: "Wine_Types",
                column: "WineTypeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wines_Wine_TypeID",
                table: "Wines",
                column: "Wine_TypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Wines_WineName_WineYear",
                table: "Wines",
                columns: new[] { "WineName", "WineYear" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Wines");

            migrationBuilder.DropTable(
                name: "Wine_Types");
        }
    }
}
