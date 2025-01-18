using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bytestrone.AppraisalSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Updatedrolebasedrelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employees_systemroles_mapping_system_roles_SystemRoleId1",
                table: "employees_systemroles_mapping");

            migrationBuilder.DropIndex(
                name: "IX_employees_systemroles_mapping_SystemRoleId1",
                table: "employees_systemroles_mapping");

            migrationBuilder.DropColumn(
                name: "SystemRoleId1",
                table: "employees_systemroles_mapping");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SystemRoleId1",
                table: "employees_systemroles_mapping",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_employees_systemroles_mapping_SystemRoleId1",
                table: "employees_systemroles_mapping",
                column: "SystemRoleId1");

            migrationBuilder.AddForeignKey(
                name: "FK_employees_systemroles_mapping_system_roles_SystemRoleId1",
                table: "employees_systemroles_mapping",
                column: "SystemRoleId1",
                principalTable: "system_roles",
                principalColumn: "Id");
        }
    }
}
