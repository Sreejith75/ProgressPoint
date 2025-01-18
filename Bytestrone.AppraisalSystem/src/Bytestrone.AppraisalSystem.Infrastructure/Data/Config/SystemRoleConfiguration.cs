using Bytestrone.AppraisalSystem.Core.SystemRoleAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bytestrone.AppraisalSystem.Infrastructure.Data.Config;

public class SystemRoleConfiguration : IEntityTypeConfiguration<SystemRole>
{
    public void Configure(EntityTypeBuilder<SystemRole> builder)
    {
        builder.ToTable("system_roles");

        builder.HasKey(sr => sr.Id);

        builder.Property(sr => sr.RoleName)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("role_name");
        builder.Property(sr => sr.Description)
            .IsRequired()
            .HasColumnName("description");

        builder.HasMany(sr => sr.EmployeeSystemRoles)
            .WithOne(esr => esr.SystemRole)
            .HasForeignKey(esr => esr.SystemRoleId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(sr => sr.SystemRolePermissions)
            .WithOne(srp => srp.SystemRole)
            .HasForeignKey(srp => srp.SystemRoleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
