using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bytestrone.AppraisalSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewchangesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_role_performancefactor_department_RoleId",
                table: "role_performancefactor");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "role_performancefactor",
                newName: "DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_role_performancefactor_RoleId",
                table: "role_performancefactor",
                newName: "IX_role_performancefactor_DepartmentId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Weightage",
                table: "role_performancefactor",
                type: "numeric(5,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddForeignKey(
                name: "FK_role_performancefactor_department_DepartmentId",
                table: "role_performancefactor",
                column: "DepartmentId",
                principalTable: "department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_role_performancefactor_department_DepartmentId",
                table: "role_performancefactor");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "role_performancefactor",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_role_performancefactor_DepartmentId",
                table: "role_performancefactor",
                newName: "IX_role_performancefactor_RoleId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Weightage",
                table: "role_performancefactor",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(5,2)");

            migrationBuilder.AddForeignKey(
                name: "FK_role_performancefactor_department_RoleId",
                table: "role_performancefactor",
                column: "RoleId",
                principalTable: "department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
