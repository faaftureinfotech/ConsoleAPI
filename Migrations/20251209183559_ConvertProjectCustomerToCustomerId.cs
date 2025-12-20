using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConstructionFinance.API.Migrations
{
    /// <inheritdoc />
    public partial class ConvertProjectCustomerToCustomerId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Customer",
                table: "Projects");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CustomerId",
                table: "Projects",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Customers_CustomerId",
                table: "Projects",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Customers_CustomerId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_CustomerId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Projects");

            migrationBuilder.AddColumn<string>(
                name: "Customer",
                table: "Projects",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
