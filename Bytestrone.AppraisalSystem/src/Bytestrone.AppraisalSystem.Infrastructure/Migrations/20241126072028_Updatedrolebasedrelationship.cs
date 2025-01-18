using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bytestrone.AppraisalSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Updatedrolebasedrelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_role_performancefactor_mapping_employee_roles_EmployeeRoleId",
                table: "role_performancefactor_mapping");

            migrationBuilder.DropIndex(
                name: "IX_role_performancefactor_mapping_EmployeeRoleId",
                table: "role_performancefactor_mapping");

            migrationBuilder.DropColumn(
                name: "EmployeeRoleId",
                table: "role_performancefactor_mapping");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeRoleId",
                table: "role_performancefactor_mapping",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_role_performancefactor_mapping_EmployeeRoleId",
                table: "role_performancefactor_mapping",
                column: "EmployeeRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_role_performancefactor_mapping_employee_roles_EmployeeRoleId",
                table: "role_performancefactor_mapping",
                column: "EmployeeRoleId",
                principalTable: "employee_roles",
                principalColumn: "Id");
        }
    }
}
