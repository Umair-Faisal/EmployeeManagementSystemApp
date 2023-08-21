using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.LocalDBMigrations
{
    /// <inheritdoc />
    public partial class EmpDateAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LeavingDate",
                table: "Employees",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartingDate",
                table: "Employees",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeavingDate",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "StartingDate",
                table: "Employees");
        }
    }
}
