using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConstructionFinance.API.Migrations
{
    /// <inheritdoc />
    public partial class AddContractAndKycColumnsToEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AadharNumber",
                table: "Employees",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "ContractEndDate",
                table: "Employees",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ContractStartDate",
                table: "Employees",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PanNumber",
                table: "Employees",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AadharNumber",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ContractEndDate",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ContractStartDate",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PanNumber",
                table: "Employees");
        }
    }
}
