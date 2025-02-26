using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DhavalTailor_Wines.Data.DWMigrations
{
    /// <inheritdoc />
    public partial class AfterAuditable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Wines",
                type: "TEXT",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Wines",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Wines",
                type: "TEXT",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "Wines",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Wine_Types",
                type: "TEXT",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Wine_Types",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Wine_Types",
                type: "TEXT",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "Wine_Types",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Wines");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Wines");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Wines");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Wines");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Wine_Types");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Wine_Types");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Wine_Types");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Wine_Types");
        }
    }
}
