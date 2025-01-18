using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Bytestrone.AppraisalSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovedNavigationalPropertyFromDepartmentnew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "role_performancefactor");

            migrationBuilder.CreateTable(
                name: "department_performancefactor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Weightage = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    DepartmentId = table.Column<int>(type: "integer", nullable: false),
                    PerformanceFactorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_department_performancefactor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_department_performancefactor_department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_department_performancefactor_performance_factor_Performance~",
                        column: x => x.PerformanceFactorId,
                        principalTable: "performance_factor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_department_performancefactor_DepartmentId",
                table: "department_performancefactor",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_department_performancefactor_PerformanceFactorId",
                table: "department_performancefactor",
                column: "PerformanceFactorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "department_performancefactor");

            migrationBuilder.CreateTable(
                name: "role_performancefactor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DepartmentId = table.Column<int>(type: "integer", nullable: false),
                    PerformanceFactorId = table.Column<int>(type: "integer", nullable: false),
                    Weightage = table.Column<decimal>(type: "numeric(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_performancefactor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_role_performancefactor_department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_role_performancefactor_performance_factor_PerformanceFactor~",
                        column: x => x.PerformanceFactorId,
                        principalTable: "performance_factor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_role_performancefactor_DepartmentId",
                table: "role_performancefactor",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_role_performancefactor_PerformanceFactorId",
                table: "role_performancefactor",
                column: "PerformanceFactorId");
        }
    }
}
