using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bytestrone.AppraisalSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedmissingfKreferencesForAppraisalForm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_appraisal_form_AppraisalCycleId",
                table: "appraisal_form",
                column: "AppraisalCycleId");

            migrationBuilder.CreateIndex(
                name: "IX_appraisal_form_DepartmentId",
                table: "appraisal_form",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_appraisal_form_appraisal_cycle_AppraisalCycleId",
                table: "appraisal_form",
                column: "AppraisalCycleId",
                principalTable: "appraisal_cycle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_appraisal_form_department_DepartmentId",
                table: "appraisal_form",
                column: "DepartmentId",
                principalTable: "department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_appraisal_form_appraisal_cycle_AppraisalCycleId",
                table: "appraisal_form");

            migrationBuilder.DropForeignKey(
                name: "FK_appraisal_form_department_DepartmentId",
                table: "appraisal_form");

            migrationBuilder.DropIndex(
                name: "IX_appraisal_form_AppraisalCycleId",
                table: "appraisal_form");

            migrationBuilder.DropIndex(
                name: "IX_appraisal_form_DepartmentId",
                table: "appraisal_form");
        }
    }
}
