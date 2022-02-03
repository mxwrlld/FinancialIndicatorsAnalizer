using Microsoft.EntityFrameworkCore.Migrations;

namespace FIADbContext.Migrations
{
    public partial class CreateDbCodeFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Enterprise",
                columns: table => new
                {
                    TIN = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LegalAddress = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enterprise", x => x.TIN);
                });

            migrationBuilder.CreateTable(
                name: "FinancialResult",
                columns: table => new
                {
                    Year = table.Column<int>(type: "int", nullable: false),
                    Quarter = table.Column<int>(type: "int", nullable: false),
                    Income = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Consumption = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EnterpriseTIN = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_FinancialResult_Enterprise_EnterpriseTIN",
                        column: x => x.EnterpriseTIN,
                        principalTable: "Enterprise",
                        principalColumn: "TIN",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinancialResult_EnterpriseTIN",
                table: "FinancialResult",
                column: "EnterpriseTIN");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinancialResult");

            migrationBuilder.DropTable(
                name: "Enterprise");
        }
    }
}
