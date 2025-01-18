using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bytestrone.AppraisalSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovedNavigationalPropertyFromDepartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_role_performancefactor_department_DepartmentId",
                table: "role_performancefactor");

            migrationBuilder.DropForeignKey(
                name: "FK_role_performancefactor_performance_factor_PerformanceFactor~",
                table: "role_performancefactor");

            migrationBuilder.AddForeignKey(
                name: "FK_role_performancefactor_department_DepartmentId",
                table: "role_performancefactor",
                column: "DepartmentId",
                principalTable: "department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_role_performancefactor_performance_factor_PerformanceFactor~",
                table: "role_performancefactor",
                column: "PerformanceFactorId",
                principalTable: "performance_factor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_role_performancefactor_department_DepartmentId",
                table: "role_performancefactor");

            migrationBuilder.DropForeignKey(
                name: "FK_role_performancefactor_performance_factor_PerformanceFactor~",
                table: "role_performancefactor");

            migrationBuilder.AddForeignKey(
                name: "FK_role_performancefactor_department_DepartmentId",
                table: "role_performancefactor",
                column: "DepartmentId",
                principalTable: "department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_role_performancefactor_performance_factor_PerformanceFactor~",
                table: "role_performancefactor",
                column: "PerformanceFactorId",
                principalTable: "performance_factor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
