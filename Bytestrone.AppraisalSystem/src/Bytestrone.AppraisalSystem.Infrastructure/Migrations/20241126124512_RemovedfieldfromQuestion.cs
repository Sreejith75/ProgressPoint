using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bytestrone.AppraisalSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovedfieldfromQuestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_question_IndicatorId",
                table: "question");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "question");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "question",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "question");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "question",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_question_IndicatorId",
                table: "question",
                column: "IndicatorId");
        }
    }
}
