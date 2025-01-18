using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Bytestrone.AppraisalSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Updations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_performance_indicator_performance_factor_PerformanceFactorId",
                table: "performance_indicator");

            migrationBuilder.DropForeignKey(
                name: "FK_role_performancefactors_employee_roles_EmployeeRoleId",
                table: "role_performancefactors");

            migrationBuilder.DropIndex(
                name: "IX_performance_indicator_PerformanceFactorId",
                table: "performance_indicator");

            migrationBuilder.DropPrimaryKey(
                name: "PK_role_performancefactors",
                table: "role_performancefactors");

            migrationBuilder.DropIndex(
                name: "IX_role_performancefactors_EmployeeRoleId",
                table: "role_performancefactors");

            migrationBuilder.DropColumn(
                name: "PerformanceFactorId",
                table: "performance_indicator");

            migrationBuilder.RenameTable(
                name: "role_performancefactors",
                newName: "rolePerformanceFactors");

            migrationBuilder.AlterColumn<string>(
                name: "PerformanceFactor",
                table: "rolePerformanceFactors",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AddPrimaryKey(
                name: "PK_rolePerformanceFactors",
                table: "rolePerformanceFactors",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "role_performancefactor_mapping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    PerformanceFactorId = table.Column<int>(type: "integer", nullable: false),
                    Weightage = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    EmployeeRoleId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_performancefactor_mapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_role_performancefactor_mapping_employee_roles_EmployeeRoleId",
                        column: x => x.EmployeeRoleId,
                        principalTable: "employee_roles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_role_performancefactor_mapping_employee_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "employee_roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_role_performancefactor_mapping_performance_factor_Performan~",
                        column: x => x.PerformanceFactorId,
                        principalTable: "performance_factor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_performance_indicator_FactorId",
                table: "performance_indicator",
                column: "FactorId");

            migrationBuilder.CreateIndex(
                name: "IX_role_performancefactor_mapping_EmployeeRoleId",
                table: "role_performancefactor_mapping",
                column: "EmployeeRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_role_performancefactor_mapping_PerformanceFactorId",
                table: "role_performancefactor_mapping",
                column: "PerformanceFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_role_performancefactor_mapping_RoleId",
                table: "role_performancefactor_mapping",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_performance_indicator_performance_factor_FactorId",
                table: "performance_indicator",
                column: "FactorId",
                principalTable: "performance_factor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_performance_indicator_performance_factor_FactorId",
                table: "performance_indicator");

            migrationBuilder.DropTable(
                name: "role_performancefactor_mapping");

            migrationBuilder.DropIndex(
                name: "IX_performance_indicator_FactorId",
                table: "performance_indicator");

            migrationBuilder.DropPrimaryKey(
                name: "PK_rolePerformanceFactors",
                table: "rolePerformanceFactors");

            migrationBuilder.RenameTable(
                name: "rolePerformanceFactors",
                newName: "role_performancefactors");

            migrationBuilder.AddColumn<int>(
                name: "PerformanceFactorId",
                table: "performance_indicator",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PerformanceFactor",
                table: "role_performancefactors",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_role_performancefactors",
                table: "role_performancefactors",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_performance_indicator_PerformanceFactorId",
                table: "performance_indicator",
                column: "PerformanceFactorId");

            migrationBuilder.CreateIndex(
                name: "IX_role_performancefactors_EmployeeRoleId",
                table: "role_performancefactors",
                column: "EmployeeRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_performance_indicator_performance_factor_PerformanceFactorId",
                table: "performance_indicator",
                column: "PerformanceFactorId",
                principalTable: "performance_factor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_role_performancefactors_employee_roles_EmployeeRoleId",
                table: "role_performancefactors",
                column: "EmployeeRoleId",
                principalTable: "employee_roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
