using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bytestrone.AppraisalSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedAppraiserIdToSummery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_appraisal_summaries_employees_EmployeeId",
                table: "appraisal_summaries");

            migrationBuilder.AddForeignKey(
                name: "FK_appraisal_summaries_employees_EmployeeId",
                table: "appraisal_summaries",
                column: "EmployeeId",
                principalTable: "employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_appraisal_summaries_employees_EmployeeId",
                table: "appraisal_summaries");

            migrationBuilder.AddForeignKey(
                name: "FK_appraisal_summaries_employees_EmployeeId",
                table: "appraisal_summaries",
                column: "EmployeeId",
                principalTable: "employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
