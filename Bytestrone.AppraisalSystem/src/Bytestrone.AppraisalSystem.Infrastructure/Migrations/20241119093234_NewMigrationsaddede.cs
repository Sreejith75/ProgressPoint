using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Bytestrone.AppraisalSystem.Infrastructure.Migrations;
/// <inheritdoc />
public partial class NewMigrationsaddede : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Contributors",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                Status = table.Column<int>(type: "integer", nullable: false),
                PhoneNumber_CountryCode = table.Column<string>(type: "text", nullable: true),
                PhoneNumber_Number = table.Column<string>(type: "text", nullable: true),
                PhoneNumber_Extension = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Contributors", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "employee_roles",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                RoleName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                EmployeeRoleCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                HierarchyLevel = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_employee_roles", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "performance_factor",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                FactorName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_performance_factor", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "permissions",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                PermissionName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                PermissionCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_permissions", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "system_roles",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                role_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_system_roles", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "role_performancefactors",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                EmployeeRoleId = table.Column<int>(type: "integer", nullable: false),
                PerformanceFactor = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                Weightage = table.Column<decimal>(type: "numeric", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_role_performancefactors", x => x.Id);
                table.ForeignKey(
                    name: "FK_role_performancefactors_employee_roles_EmployeeRoleId",
                    column: x => x.EmployeeRoleId,
                    principalTable: "employee_roles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "performance_indicator",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                IndicatorName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                FactorId = table.Column<int>(type: "integer", nullable: false),
                PerformanceFactorId = table.Column<int>(type: "integer", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_performance_indicator", x => x.Id);
                table.ForeignKey(
                    name: "FK_performance_indicator_performance_factor_PerformanceFactorId",
                    column: x => x.PerformanceFactorId,
                    principalTable: "performance_factor",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "employees",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                EmployeeRoleId = table.Column<int>(type: "integer", nullable: true),
                AppraiserId = table.Column<int>(type: "integer", nullable: true),
                SystemRoleId = table.Column<int>(type: "integer", nullable: true),
                PasswordHash = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                isActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                ModifyOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_employees", x => x.Id);
                table.ForeignKey(
                    name: "FK_employees_employee_roles_EmployeeRoleId",
                    column: x => x.EmployeeRoleId,
                    principalTable: "employee_roles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.SetNull);
                table.ForeignKey(
                    name: "FK_employees_employees_AppraiserId",
                    column: x => x.AppraiserId,
                    principalTable: "employees",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_employees_system_roles_SystemRoleId",
                    column: x => x.SystemRoleId,
                    principalTable: "system_roles",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "systemrole_permissions",
            columns: table => new
            {
                SystemRoleId = table.Column<int>(type: "integer", nullable: false),
                PermissionId = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_systemrole_permissions", x => new { x.SystemRoleId, x.PermissionId });
                table.ForeignKey(
                    name: "FK_systemrole_permissions_permissions_PermissionId",
                    column: x => x.PermissionId,
                    principalTable: "permissions",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_systemrole_permissions_system_roles_SystemRoleId",
                    column: x => x.SystemRoleId,
                    principalTable: "system_roles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "question",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                QuestionText = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                IsActive = table.Column<bool>(type: "boolean", nullable: false),
                IndicatorId = table.Column<int>(type: "integer", nullable: false),
                PerformanceIndicatorId = table.Column<int>(type: "integer", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_question", x => x.Id);
                table.ForeignKey(
                    name: "FK_question_performance_indicator_PerformanceIndicatorId",
                    column: x => x.PerformanceIndicatorId,
                    principalTable: "performance_indicator",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "employee_appraiser_mappings",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                EmployeeId = table.Column<int>(type: "integer", nullable: false),
                EmployeeId1 = table.Column<int>(type: "integer", nullable: true),
                AppraiserId = table.Column<int>(type: "integer", nullable: false),
                EffectiveDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                ChangedReason = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                CreatedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                UpdatedBy = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_employee_appraiser_mappings", x => x.Id);
                table.ForeignKey(
                    name: "FK_employee_appraiser_mappings_employees_AppraiserId",
                    column: x => x.AppraiserId,
                    principalTable: "employees",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_employee_appraiser_mappings_employees_EmployeeId",
                    column: x => x.EmployeeId,
                    principalTable: "employees",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_employee_appraiser_mappings_employees_EmployeeId1",
                    column: x => x.EmployeeId1,
                    principalTable: "employees",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "employee_systemroles",
            columns: table => new
            {
                EmployeeId = table.Column<int>(type: "integer", nullable: false),
                SystemRoleId = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_employee_systemroles", x => new { x.EmployeeId, x.SystemRoleId });
                table.ForeignKey(
                    name: "FK_employee_systemroles_employees_EmployeeId",
                    column: x => x.EmployeeId,
                    principalTable: "employees",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_employee_systemroles_system_roles_SystemRoleId",
                    column: x => x.SystemRoleId,
                    principalTable: "system_roles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_employee_appraiser_mappings_AppraiserId",
            table: "employee_appraiser_mappings",
            column: "AppraiserId");

        migrationBuilder.CreateIndex(
            name: "IX_employee_appraiser_mappings_EmployeeId",
            table: "employee_appraiser_mappings",
            column: "EmployeeId");

        migrationBuilder.CreateIndex(
            name: "IX_employee_appraiser_mappings_EmployeeId1",
            table: "employee_appraiser_mappings",
            column: "EmployeeId1");

        migrationBuilder.CreateIndex(
            name: "IX_employee_systemroles_SystemRoleId",
            table: "employee_systemroles",
            column: "SystemRoleId");

        migrationBuilder.CreateIndex(
            name: "IX_employees_AppraiserId",
            table: "employees",
            column: "AppraiserId");

        migrationBuilder.CreateIndex(
            name: "IX_employees_EmployeeRoleId",
            table: "employees",
            column: "EmployeeRoleId");

        migrationBuilder.CreateIndex(
            name: "IX_employees_SystemRoleId",
            table: "employees",
            column: "SystemRoleId");

        migrationBuilder.CreateIndex(
            name: "IX_performance_indicator_PerformanceFactorId",
            table: "performance_indicator",
            column: "PerformanceFactorId");

        migrationBuilder.CreateIndex(
            name: "IX_question_IndicatorId",
            table: "question",
            column: "IndicatorId");

        migrationBuilder.CreateIndex(
            name: "IX_question_PerformanceIndicatorId",
            table: "question",
            column: "PerformanceIndicatorId");

        migrationBuilder.CreateIndex(
            name: "IX_role_performancefactors_EmployeeRoleId",
            table: "role_performancefactors",
            column: "EmployeeRoleId");

        migrationBuilder.CreateIndex(
            name: "IX_systemrole_permissions_PermissionId",
            table: "systemrole_permissions",
            column: "PermissionId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Contributors");

        migrationBuilder.DropTable(
            name: "employee_appraiser_mappings");

        migrationBuilder.DropTable(
            name: "employee_systemroles");

        migrationBuilder.DropTable(
            name: "question");

        migrationBuilder.DropTable(
            name: "role_performancefactors");

        migrationBuilder.DropTable(
            name: "systemrole_permissions");

        migrationBuilder.DropTable(
            name: "employees");

        migrationBuilder.DropTable(
            name: "performance_indicator");

        migrationBuilder.DropTable(
            name: "permissions");

        migrationBuilder.DropTable(
            name: "employee_roles");

        migrationBuilder.DropTable(
            name: "system_roles");

        migrationBuilder.DropTable(
            name: "performance_factor");
    }
}
