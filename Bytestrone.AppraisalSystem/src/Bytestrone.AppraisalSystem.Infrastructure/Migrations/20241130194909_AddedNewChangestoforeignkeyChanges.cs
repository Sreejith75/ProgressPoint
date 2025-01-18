using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bytestrone.AppraisalSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewChangestoforeignkeyChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_performance_indicator_performance_factor_FactorId",
                table: "performance_indicator");

            migrationBuilder.AlterColumn<decimal>(
                name: "Weightage",
                table: "performance_indicator",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<string>(
                name: "IndicatorName",
                table: "performance_indicator",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "Default Indicator",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddForeignKey(
                name: "FK_performance_indicator_performance_factor_FactorId",
                table: "performance_indicator",
                column: "FactorId",
                principalTable: "performance_factor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_performance_indicator_performance_factor_FactorId",
                table: "performance_indicator");

            migrationBuilder.AlterColumn<decimal>(
                name: "Weightage",
                table: "performance_indicator",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "IndicatorName",
                table: "performance_indicator",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldDefaultValue: "Default Indicator");

            migrationBuilder.AddForeignKey(
                name: "FK_performance_indicator_performance_factor_FactorId",
                table: "performance_indicator",
                column: "FactorId",
                principalTable: "performance_factor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
