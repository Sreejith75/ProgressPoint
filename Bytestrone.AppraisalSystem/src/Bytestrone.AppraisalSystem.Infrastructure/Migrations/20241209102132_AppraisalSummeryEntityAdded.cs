using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Bytestrone.AppraisalSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AppraisalSummeryEntityAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SummaryId",
                table: "appraisal_feedbacks",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "appraisal_summaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmployeeId = table.Column<int>(type: "integer", nullable: false),
                    CycleId = table.Column<int>(type: "integer", nullable: false),
                    AppraiseeScore = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    AppraiserScore = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    PerformanceBucket = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appraisal_summaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_appraisal_summaries_appraisal_cycle_CycleId",
                        column: x => x.CycleId,
                        principalTable: "appraisal_cycle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_appraisal_summaries_employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_appraisal_feedbacks_AppraiserId",
                table: "appraisal_feedbacks",
                column: "AppraiserId");

            migrationBuilder.CreateIndex(
                name: "IX_appraisal_feedbacks_EmployeeId",
                table: "appraisal_feedbacks",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_appraisal_feedbacks_SummaryId",
                table: "appraisal_feedbacks",
                column: "SummaryId");

            migrationBuilder.CreateIndex(
                name: "IX_appraisal_summaries_CycleId",
                table: "appraisal_summaries",
                column: "CycleId");

            migrationBuilder.CreateIndex(
                name: "IX_appraisal_summaries_EmployeeId",
                table: "appraisal_summaries",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_appraisal_feedbacks_appraisal_summaries_SummaryId",
                table: "appraisal_feedbacks",
                column: "SummaryId",
                principalTable: "appraisal_summaries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_appraisal_feedbacks_appraisal_summaries_SummaryId",
                table: "appraisal_feedbacks");

            migrationBuilder.DropTable(
                name: "appraisal_summaries");

            migrationBuilder.DropIndex(
                name: "IX_appraisal_feedbacks_AppraiserId",
                table: "appraisal_feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_appraisal_feedbacks_EmployeeId",
                table: "appraisal_feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_appraisal_feedbacks_SummaryId",
                table: "appraisal_feedbacks");

            migrationBuilder.DropColumn(
                name: "SummaryId",
                table: "appraisal_feedbacks");
        }
    }
}
