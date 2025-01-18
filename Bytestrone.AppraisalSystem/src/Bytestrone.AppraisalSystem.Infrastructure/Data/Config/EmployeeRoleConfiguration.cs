using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bytestrone.AppraisalSystem.Core.Entities.EmployeeRoleAggregate;

namespace Bytestrone.AppraisalSystem.Infrastructure.Data.Config;
public class EmployeeRoleConfiguration : IEntityTypeConfiguration<EmployeeRole>
{
    public void Configure(EntityTypeBuilder<EmployeeRole> builder)
    {
        // Configure the table
        builder.ToTable("employee_roles");

        // Primary Key
        builder.HasKey(er => er.Id);

        // Properties
        builder.Property(er => er.RoleName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(er => er.EmployeeRoleCode)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(er => er.HierarchyLevel)
            .IsRequired();

        builder.HasOne(er => er.Department)
            .WithMany()  // Navigation property in Department
            .HasForeignKey(er => er.DepartmentId)  // Foreign key in EmployeeRole
            .OnDelete(DeleteBehavior.Cascade);
            
        builder.Property(er=>er.DepartmentId).HasColumnName("departmentId");
}}