using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bytestrone.AppraisalSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewUpdationsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employees_systemroles_mapping_employees_EmployeeId1",
                table: "employees_systemroles_mapping");

            migrationBuilder.RenameColumn(
                name: "EmployeeId1",
                table: "employees_systemroles_mapping",
                newName: "SystemRoleId1");

            migrationBuilder.RenameIndex(
                name: "IX_employees_systemroles_mapping_EmployeeId1",
                table: "employees_systemroles_mapping",
                newName: "IX_employees_systemroles_mapping_SystemRoleId1");

            migrationBuilder.AddForeignKey(
                name: "FK_employees_systemroles_mapping_system_roles_SystemRoleId1",
                table: "employees_systemroles_mapping",
                column: "SystemRoleId1",
                principalTable: "system_roles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employees_systemroles_mapping_system_roles_SystemRoleId1",
                table: "employees_systemroles_mapping");

            migrationBuilder.RenameColumn(
                name: "SystemRoleId1",
                table: "employees_systemroles_mapping",
                newName: "EmployeeId1");

            migrationBuilder.RenameIndex(
                name: "IX_employees_systemroles_mapping_SystemRoleId1",
                table: "employees_systemroles_mapping",
                newName: "IX_employees_systemroles_mapping_EmployeeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_employees_systemroles_mapping_employees_EmployeeId1",
                table: "employees_systemroles_mapping",
                column: "EmployeeId1",
                principalTable: "employees",
                principalColumn: "Id");
        }
    }
}
