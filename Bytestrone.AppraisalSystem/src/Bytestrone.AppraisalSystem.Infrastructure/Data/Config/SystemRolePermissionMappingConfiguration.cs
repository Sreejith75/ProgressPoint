using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bytestrone.AppraisalSystem.Core.SystemRoleAggregate;

namespace Bytestrone.AppraisalSystem.Infrastructure.Data.Config;
public class SystemRolePermissionMappingConfiguration : IEntityTypeConfiguration<SystemRolePermission>
{
    public void Configure(EntityTypeBuilder<SystemRolePermission> builder)
    {
        builder.ToTable("systemrole_permissions");

        builder.HasKey(sr => new { sr.SystemRoleId, sr.PermissionId });

        builder.HasOne(sr => sr.SystemRole)
               .WithMany(role => role.SystemRolePermissions)
               .HasForeignKey(sr => sr.SystemRoleId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(sr => sr.Permission)
               .WithMany(permission => permission.SystemRolePermissions)
               .HasForeignKey(sr => sr.PermissionId)
               .OnDelete(DeleteBehavior.Cascade);

    }
}
