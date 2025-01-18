using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bytestrone.AppraisalSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovedFormIdFromAppraisalFeedback : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_appraisal_feedbacks_appraisal_form_FormId",
                table: "appraisal_feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_appraisal_feedbacks_FormId",
                table: "appraisal_feedbacks");

            migrationBuilder.DropColumn(
                name: "FormId",
                table: "appraisal_feedbacks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FormId",
                table: "appraisal_feedbacks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_appraisal_feedbacks_FormId",
                table: "appraisal_feedbacks",
                column: "FormId");

            migrationBuilder.AddForeignKey(
                name: "FK_appraisal_feedbacks_appraisal_form_FormId",
                table: "appraisal_feedbacks",
                column: "FormId",
                principalTable: "appraisal_form",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
