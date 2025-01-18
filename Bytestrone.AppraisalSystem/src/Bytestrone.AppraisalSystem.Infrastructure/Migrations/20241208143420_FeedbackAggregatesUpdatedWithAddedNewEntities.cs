using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Bytestrone.AppraisalSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FeedbackAggregatesUpdatedWithAddedNewEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "appraisal_feedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FormId = table.Column<int>(type: "integer", nullable: false),
                    EmployeeId = table.Column<int>(type: "integer", nullable: false),
                    AppraiserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appraisal_feedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_appraisal_feedbacks_appraisal_form_FormId",
                        column: x => x.FormId,
                        principalTable: "appraisal_form",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "appraisal_feedback_details",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FeedbackId = table.Column<int>(type: "integer", nullable: false),
                    QuestionId = table.Column<int>(type: "integer", nullable: false),
                    AppraiseeRating = table.Column<int>(type: "integer", nullable: false),
                    AppraiseeComment = table.Column<string>(type: "text", nullable: false),
                    Artifact_FilePath = table.Column<string>(type: "text", nullable: true),
                    Artifact_FileType = table.Column<string>(type: "text", nullable: true),
                    AppraiserRating = table.Column<int>(type: "integer", nullable: true),
                    AppraiserComment = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appraisal_feedback_details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_appraisal_feedback_details_appraisal_feedbacks_FeedbackId",
                        column: x => x.FeedbackId,
                        principalTable: "appraisal_feedbacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_appraisal_feedback_details_FeedbackId",
                table: "appraisal_feedback_details",
                column: "FeedbackId");

            migrationBuilder.CreateIndex(
                name: "IX_appraisal_feedback_details_QuestionId",
                table: "appraisal_feedback_details",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_appraisal_feedbacks_FormId",
                table: "appraisal_feedbacks",
                column: "FormId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
