using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bytestrone.AppraisalSystem.Core.EmployeeAggregate;

namespace Bytestrone.AppraisalSystem.Infrastructure.Data.Config;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("employees");

        // Primary key
        builder.HasKey(e => e.Id);

        // Properties
        builder.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(e => e.LastName).IsRequired().HasMaxLength(50);
        builder.Property(e => e.Email).IsRequired().HasMaxLength(100);
        builder.Property(e => e.PasswordHash).IsRequired();
        builder.Property(e => e.PhoneNumber).HasMaxLength(15);

        // Auditing fields
        builder.Property(e => e.CreatedOn).IsRequired();
        builder.Property(e => e.ModifiedOn).IsRequired(false);

        // Relationships
        // Appraisers relationship
        builder
            .HasMany(e => e.Appraisers)
            .WithOne(am => am.Appraiser)
            .HasForeignKey(am => am.AppraiserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Appraisees relationship
        builder
            .HasMany(e => e.Appraisees)
            .WithOne(am => am.Appraisee)
            .HasForeignKey(am => am.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);


        builder
            .HasMany(e => e.SystemRoles)
            .WithOne()
            .HasForeignKey(sr => sr.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasOne(e => e.Role)
            .WithMany()
            .HasForeignKey(e => e.EmployeeRoleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
