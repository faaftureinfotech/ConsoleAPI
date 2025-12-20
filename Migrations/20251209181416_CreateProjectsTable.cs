using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ConstructionFinance.API.Migrations
{
    /// <inheritdoc />
    public partial class CreateProjectsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    // ✅ This annotation is ignored by MySQL but SAFE to keep
                    // ✅ Or you can remove it entirely — both are fine

                    ProjectName = table.Column<string>(maxLength: 150, nullable: false),
                    Customer = table.Column<string>(maxLength: 150, nullable: false),
                    ProjectType = table.Column<string>(maxLength: 100, nullable: false),
                    Location = table.Column<string>(maxLength: 150, nullable: false),

                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true),

                    Status = table.Column<string>(maxLength: 50, nullable: false),

                    ContractValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AdvanceReceived = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RemainingAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),

                    PaymentTerms = table.Column<string>(maxLength: 250, nullable: true),
                    ProjectManager = table.Column<string>(maxLength: 150, nullable: true),
                    Contractor = table.Column<string>(maxLength: 150, nullable: true),

                    Notes = table.Column<string>(maxLength: 500, nullable: true),

                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Projects");
        }

    }
}
