using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bytestrone.AppraisalSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedQuestionReferenceToAppraisalFeedbackdetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_appraisal_feedback_details_question_QuestionId",
                table: "appraisal_feedback_details",
                column: "QuestionId",
                principalTable: "question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_appraisal_feedback_details_question_QuestionId",
                table: "appraisal_feedback_details");
        }
    }
}
