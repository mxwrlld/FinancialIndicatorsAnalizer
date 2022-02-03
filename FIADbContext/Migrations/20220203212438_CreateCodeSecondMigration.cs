using Microsoft.EntityFrameworkCore.Migrations;

namespace FIADbContext.Migrations
{
    public partial class CreateCodeSecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Enterprise",
                columns: table => new
                {
                    TIN = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Quarter = table.Column<int>(type: "int", nullable: false),
                    Income = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Consumption = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Enterprise = table.Column<string>(type: "nvarchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinancialResult_Enterprise_Enterprise",
                        column: x => x.Enterprise,
                        principalTable: "Enterprise",
                        principalColumn: "TIN",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinancialResult_Enterprise",
                table: "FinancialResult",
                column: "Enterprise");
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
