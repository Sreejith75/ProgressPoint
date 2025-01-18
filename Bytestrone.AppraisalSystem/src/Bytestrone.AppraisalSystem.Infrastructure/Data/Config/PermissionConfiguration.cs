using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bytestrone.AppraisalSystem.Core.PermissionAggregate;

namespace Bytestrone.AppraisalSystem.Infrastructure.Data.Config;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("permissions");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.PermissionName)
               .IsRequired()
               .HasMaxLength(255);

        builder.Property(p => p.PermissionCode)
               .IsRequired()
               .HasMaxLength(50); 

        builder.HasMany(p => p.SystemRolePermissions)
               .WithOne()
               .HasForeignKey(sr => sr.PermissionId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
