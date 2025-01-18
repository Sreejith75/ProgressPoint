using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bytestrone.AppraisalSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovedAppraisalCycleFromAppraisalForm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_appraisal_form_appraisal_cycle_AppraisalCycleId",
                table: "appraisal_form");

            migrationBuilder.DropIndex(
                name: "IX_appraisal_form_AppraisalCycleId",
                table: "appraisal_form");

            migrationBuilder.DropColumn(
                name: "AppraisalCycleId",
                table: "appraisal_form");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppraisalCycleId",
                table: "appraisal_form",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_appraisal_form_AppraisalCycleId",
                table: "appraisal_form",
                column: "AppraisalCycleId");

            migrationBuilder.AddForeignKey(
                name: "FK_appraisal_form_appraisal_cycle_AppraisalCycleId",
                table: "appraisal_form",
                column: "AppraisalCycleId",
                principalTable: "appraisal_cycle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
