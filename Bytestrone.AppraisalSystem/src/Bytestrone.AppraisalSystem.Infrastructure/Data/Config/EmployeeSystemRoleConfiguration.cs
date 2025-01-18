using Bytestrone.AppraisalSystem.Core.EmployeeAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class EmployeeSystemRoleConfiguration : IEntityTypeConfiguration<EmployeeSystemRole>
{
    public void Configure(EntityTypeBuilder<EmployeeSystemRole> builder)
    {
        builder.ToTable("employees_systemroles_mapping");

        builder.HasKey(esr => new { esr.EmployeeId, esr.SystemRoleId });

        builder.HasOne(esr => esr.Employee)
               .WithMany(e => e.SystemRoles)
               .HasForeignKey(esr => esr.EmployeeId)
               .OnDelete(DeleteBehavior.Cascade); 

        builder.HasOne(esr => esr.SystemRole)
               .WithMany(sr=>sr.EmployeeSystemRoles)
               .HasForeignKey(esr => esr.SystemRoleId )
               .OnDelete(DeleteBehavior.Cascade);

        builder.Property(esr => esr.SystemRoleId).IsRequired();
    }
}
