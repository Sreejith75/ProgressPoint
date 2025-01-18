﻿// <auto-generated />
using System;
using Bytestrone.AppraisalSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Bytestrone.AppraisalSystem.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241119132547_NewUpdations")]
    partial class NewUpdations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Bytestrone.AppraisalSystem.Core.ContributorAggregate.Contributor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Contributors");
                });

            modelBuilder.Entity("Bytestrone.AppraisalSystem.Core.EmployeeAggregate.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)");

                    b.HasKey("Id");

                    b.ToTable("employees", (string)null);
                });

            modelBuilder.Entity("Bytestrone.AppraisalSystem.Core.EmployeeAggregate.EmployeeSystemRole", b =>
                {
                    b.Property<int>("EmployeeId")
                        .HasColumnType("integer");

                    b.Property<int>("SystemRoleId")
                        .HasColumnType("integer");

                    b.Property<int?>("EmployeeId1")
                        .HasColumnType("integer");

                    b.HasKey("EmployeeId", "SystemRoleId");

                    b.HasIndex("EmployeeId1");

                    b.HasIndex("SystemRoleId");

                    b.ToTable("employees_systemroles_mapping", (string)null);
                });

            modelBuilder.Entity("Bytestrone.AppraisalSystem.Core.EmployeeRoleAggregate.EmployeeRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("EmployeeRoleCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("HierarchyLevel")
                        .HasColumnType("integer");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("employee_roles", (string)null);
                });

            modelBuilder.Entity("Bytestrone.AppraisalSystem.Core.Entities.EmployeeAggregate.EmployeeAppraiserMapping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AppraiserId")
                        .HasColumnType("integer");

                    b.Property<string>("ChangedReason")
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<DateTime>("EffectiveDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AppraiserId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("employee_appraiser_mappings", (string)null);
                });

            modelBuilder.Entity("Bytestrone.AppraisalSystem.Core.PerformanceFactorAggregate.PerformanceFactor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("FactorName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("performance_factor", (string)null);
                });

            modelBuilder.Entity("Bytestrone.AppraisalSystem.Core.PerformanceFactorAggregate.PerformanceIndicator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("FactorId")
                        .HasColumnType("integer");

                    b.Property<string>("IndicatorName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("FactorId");

                    b.ToTable("performance_indicator", (string)null);
                });

            modelBuilder.Entity("Bytestrone.AppraisalSystem.Core.PerformanceFactorAggregate.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("IndicatorId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int?>("PerformanceIndicatorId")
                        .HasColumnType("integer");

                    b.Property<string>("QuestionText")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.HasKey("Id");

                    b.HasIndex("IndicatorId");

                    b.HasIndex("PerformanceIndicatorId");

                    b.ToTable("question", (string)null);
                });

            modelBuilder.Entity("Bytestrone.AppraisalSystem.Core.PermissionAggregate.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("PermissionCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("PermissionName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.ToTable("permissions", (string)null);
                });

            modelBuilder.Entity("Bytestrone.AppraisalSystem.Core.RolePerformanceFactorAggregate.RolePerformanceFactor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("EmployeeRoleId")
                        .HasColumnType("integer");

                    b.Property<string>("PerformanceFactor")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Weightage")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("rolePerformanceFactors");
                });

            modelBuilder.Entity("Bytestrone.AppraisalSystem.Core.SystemRoleAggregate.SystemRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("role_name");

                    b.HasKey("Id");

                    b.ToTable("system_roles", (string)null);
                });

            modelBuilder.Entity("Bytestrone.AppraisalSystem.Core.SystemRoleAggregate.SystemRolePermission", b =>
                {
                    b.Property<int>("SystemRoleId")
                        .HasColumnType("integer");

                    b.Property<int>("PermissionId")
                        .HasColumnType("integer");

                    b.HasKey("SystemRoleId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("systemrole_permissions", (string)null);
                });

            modelBuilder.Entity("RolePerformanceFactorWeightage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("EmployeeRoleId")
                        .HasColumnType("integer");

                    b.Property<int>("PerformanceFactorId")
                        .HasColumnType("integer");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Weightage")
                        .HasColumnType("decimal(5, 2)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeRoleId");

                    b.HasIndex("PerformanceFactorId");

                    b.HasIndex("RoleId");

                    b.ToTable("role_performancefactor_mapping", (string)null);
                });

            modelBuilder.Entity("Bytestrone.AppraisalSystem.Core.ContributorAggregate.Contributor", b =>
                {
                    b.OwnsOne("Bytestrone.AppraisalSystem.Core.ContributorAggregate.PhoneNumber", "PhoneNumber", b1 =>
                        {
                            b1.Property<int>("ContributorId")
                                .HasColumnType("integer");

                            b1.Property<string>("CountryCode")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Extension")
                                .HasColumnType("text");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("ContributorId");

                            b1.ToTable("Contributors");

                            b1.WithOwner()
                                .HasForeignKey("ContributorId");
                        });

                    b.Navigation("PhoneNumber");
                });

            modelBuilder.Entity("Bytestrone.AppraisalSystem.Core.EmployeeAggregate.EmployeeSystemRole", b =>
                {
                    b.HasOne("Bytestrone.AppraisalSystem.Core.EmployeeAggregate.Employee", null)
                        .WithMany("SystemRoles")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bytestrone.AppraisalSystem.Core.EmployeeAggregate.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId1");

                    b.HasOne("Bytestrone.AppraisalSystem.Core.SystemRoleAggregate.SystemRole", "SystemRole")
                        .WithMany("EmployeeSystemRoles")
                        .HasForeignKey("SystemRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("SystemRole");
                });

            modelBuilder.Entity("Bytestrone.AppraisalSystem.Core.Entities.EmployeeAggregate.EmployeeAppraiserMapping", b =>
                {
                    b.HasOne("Bytestrone.AppraisalSystem.Core.EmployeeAggregate.Employee", "Appraiser")
                        .WithMany()
                        .HasForeignKey("AppraiserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Bytestrone.AppraisalSystem.Core.EmployeeAggregate.Employee", "Employee")
                        .WithMany("AppraiserMappings")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appraiser");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Bytestrone.AppraisalSystem.Core.PerformanceFactorAggregate.PerformanceIndicator", b =>
                {
                    b.HasOne("Bytestrone.AppraisalSystem.Core.PerformanceFactorAggregate.PerformanceFactor", null)
                        .WithMany("Indicators")
                        .HasForeignKey("FactorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Bytestrone.AppraisalSystem.Core.PerformanceFactorAggregate.Question", b =>
                {
                    b.HasOne("Bytestrone.AppraisalSystem.Core.PerformanceFactorAggregate.PerformanceIndicator", null)
                        .WithMany("Questions")
                        .HasForeignKey("PerformanceIndicatorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Bytestrone.AppraisalSystem.Core.SystemRoleAggregate.SystemRolePermission", b =>
                {
                    b.HasOne("Bytestrone.AppraisalSystem.Core.PermissionAggregate.Permission", "Permission")
                        .WithMany("SystemRolePermissions")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bytestrone.AppraisalSystem.Core.SystemRoleAggregate.SystemRole", "SystemRole")
                        .WithMany("SystemRolePermissions")
                        .HasForeignKey("SystemRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("SystemRole");
                });

            modelBuilder.Entity("RolePerformanceFactorWeightage", b =>
                {
                    b.HasOne("Bytestrone.AppraisalSystem.Core.EmployeeRoleAggregate.EmployeeRole", null)
                        .WithMany("PerformanceFactors")
                        .HasForeignKey("EmployeeRoleId");

                    b.HasOne("Bytestrone.AppraisalSystem.Core.PerformanceFactorAggregate.PerformanceFactor", null)
                        .WithMany()
                        .HasForeignKey("PerformanceFactorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Bytestrone.AppraisalSystem.Core.EmployeeRoleAggregate.EmployeeRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Bytestrone.AppraisalSystem.Core.EmployeeAggregate.Employee", b =>
                {
                    b.Navigation("AppraiserMappings");

                    b.Navigation("SystemRoles");
                });

            modelBuilder.Entity("Bytestrone.AppraisalSystem.Core.EmployeeRoleAggregate.EmployeeRole", b =>
                {
                    b.Navigation("PerformanceFactors");
                });

            modelBuilder.Entity("Bytestrone.AppraisalSystem.Core.PerformanceFactorAggregate.PerformanceFactor", b =>
                {
                    b.Navigation("Indicators");
                });

            modelBuilder.Entity("Bytestrone.AppraisalSystem.Core.PerformanceFactorAggregate.PerformanceIndicator", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("Bytestrone.AppraisalSystem.Core.PermissionAggregate.Permission", b =>
                {
                    b.Navigation("SystemRolePermissions");
                });

            modelBuilder.Entity("Bytestrone.AppraisalSystem.Core.SystemRoleAggregate.SystemRole", b =>
                {
                    b.Navigation("EmployeeSystemRoles");

                    b.Navigation("SystemRolePermissions");
                });
#pragma warning restore 612, 618
        }
    }
}
