using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bytestrone.AppraisalSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedAppraisalFeedbackWithEmployeeRelationNew : Migration
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_role_performancefactor",
                table: "role_performancefactor");

            migrationBuilder.RenameTable(
                name: "role_performancefactor",
                newName: "role_performancefactor");

            migrationBuilder.RenameIndex(
                name: "IX_role_performancefactor_PerformanceFactorId",
                table: "role_performancefactor",
                newName: "IX_role_performancefactor_PerformanceFactorId");

            migrationBuilder.RenameIndex(
                name: "IX_role_performancefactor_DepartmentId",
                table: "role_performancefactor",
                newName: "IX_role_performancefactor_DepartmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_role_performancefactor",
                table: "role_performancefactor",
                column: "Id");

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
                onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_role_performancefactor",
                table: "role_performancefactor");

            migrationBuilder.RenameTable(
                name: "role_performancefactor",
                newName: "role_performancefactor");

            migrationBuilder.RenameIndex(
                name: "IX_role_performancefactor_PerformanceFactorId",
                table: "role_performancefactor",
                newName: "IX_role_performancefactors_PerformanceFactorId");

            migrationBuilder.RenameIndex(
                name: "IX_role_performancefactor_DepartmentId",
                table: "role_performancefactor",
                newName: "IX_role_performancefactors_DepartmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_role_performancefactor",
                table: "role_performancefactor",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_role_performancefactors_department_DepartmentId",
                table: "role_performancefactor",
                column: "DepartmentId",
                principalTable: "department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_role_performancefactor_performance_factor_PerformanceFact~",
                table: "role_performancefactor",
                column: "PerformanceFactorId",
                principalTable: "performance_factor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
