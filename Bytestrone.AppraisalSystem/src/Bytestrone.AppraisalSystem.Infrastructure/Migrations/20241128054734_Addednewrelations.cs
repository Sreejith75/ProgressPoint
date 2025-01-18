using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bytestrone.AppraisalSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Addednewrelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_appraisal_form_department_DepartmentId",
                table: "appraisal_form");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "appraisal_form",
                newName: "EmployeeRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_appraisal_form_DepartmentId",
                table: "appraisal_form",
                newName: "IX_appraisal_form_EmployeeRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_appraisal_form_employee_roles_EmployeeRoleId",
                table: "appraisal_form",
                column: "EmployeeRoleId",
                principalTable: "employee_roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_role_performancefactor_department_RoleId",
                table: "role_performancefactor",
                column: "RoleId",
                principalTable: "department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_appraisal_form_employee_roles_EmployeeRoleId",
                table: "appraisal_form");

            migrationBuilder.DropForeignKey(
                name: "FK_role_performancefactor_department_RoleId",
                table: "role_performancefactor");

            migrationBuilder.RenameColumn(
                name: "EmployeeRoleId",
                table: "appraisal_form",
                newName: "DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_appraisal_form_EmployeeRoleId",
                table: "appraisal_form",
                newName: "IX_appraisal_form_DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_appraisal_form_department_DepartmentId",
                table: "appraisal_form",
                column: "DepartmentId",
                principalTable: "department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
