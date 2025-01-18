using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bytestrone.AppraisalSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedEmplpoyeeApraiserMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employee_appraiser_mappings_employees_AppraiserId",
                table: "employee_appraiser_mappings");

            migrationBuilder.DropForeignKey(
                name: "FK_employee_appraiser_mappings_employees_EmployeeId",
                table: "employee_appraiser_mappings");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "employee_appraiser_mappings",
                newName: "Appraisee_Id");

            migrationBuilder.RenameColumn(
                name: "AppraiserId",
                table: "employee_appraiser_mappings",
                newName: "Appraiser_Id");

            migrationBuilder.RenameIndex(
                name: "IX_employee_appraiser_mappings_EmployeeId",
                table: "employee_appraiser_mappings",
                newName: "IX_employee_appraiser_mappings_Appraisee_Id");

            migrationBuilder.RenameIndex(
                name: "IX_employee_appraiser_mappings_AppraiserId",
                table: "employee_appraiser_mappings",
                newName: "IX_employee_appraiser_mappings_Appraiser_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_employee_appraiser_mappings_employees_Appraisee_Id",
                table: "employee_appraiser_mappings",
                column: "Appraisee_Id",
                principalTable: "employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_employee_appraiser_mappings_employees_Appraiser_Id",
                table: "employee_appraiser_mappings",
                column: "Appraiser_Id",
                principalTable: "employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employee_appraiser_mappings_employees_Appraisee_Id",
                table: "employee_appraiser_mappings");

            migrationBuilder.DropForeignKey(
                name: "FK_employee_appraiser_mappings_employees_Appraiser_Id",
                table: "employee_appraiser_mappings");

            migrationBuilder.RenameColumn(
                name: "Appraiser_Id",
                table: "employee_appraiser_mappings",
                newName: "AppraiserId");

            migrationBuilder.RenameColumn(
                name: "Appraisee_Id",
                table: "employee_appraiser_mappings",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_employee_appraiser_mappings_Appraiser_Id",
                table: "employee_appraiser_mappings",
                newName: "IX_employee_appraiser_mappings_AppraiserId");

            migrationBuilder.RenameIndex(
                name: "IX_employee_appraiser_mappings_Appraisee_Id",
                table: "employee_appraiser_mappings",
                newName: "IX_employee_appraiser_mappings_EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_employee_appraiser_mappings_employees_AppraiserId",
                table: "employee_appraiser_mappings",
                column: "AppraiserId",
                principalTable: "employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_employee_appraiser_mappings_employees_EmployeeId",
                table: "employee_appraiser_mappings",
                column: "EmployeeId",
                principalTable: "employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
