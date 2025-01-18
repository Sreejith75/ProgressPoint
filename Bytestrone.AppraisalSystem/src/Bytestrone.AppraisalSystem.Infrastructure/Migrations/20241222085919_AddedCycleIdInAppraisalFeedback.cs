using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bytestrone.AppraisalSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedCycleIdInAppraisalFeedback : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CycleId",
                table: "appraisal_feedbacks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_appraisal_feedbacks_CycleId",
                table: "appraisal_feedbacks",
                column: "CycleId");

            migrationBuilder.AddForeignKey(
                name: "FK_appraisal_feedbacks_appraisal_cycle_CycleId",
                table: "appraisal_feedbacks",
                column: "CycleId",
                principalTable: "appraisal_cycle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_appraisal_feedbacks_appraisal_cycle_CycleId",
                table: "appraisal_feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_appraisal_feedbacks_CycleId",
                table: "appraisal_feedbacks");

            migrationBuilder.DropColumn(
                name: "CycleId",
                table: "appraisal_feedbacks");
        }
    }
}
