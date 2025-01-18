using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bytestrone.AppraisalSystem.Core.Entities.PerformanceFactorAggregate;

namespace Bytestrone.AppraisalSystem.Infrastructure.Data.Config;

public class RolePerformanceFactorConfiguration : IEntityTypeConfiguration<DepartmentPerformanceFactor>
{
    public void Configure(EntityTypeBuilder<DepartmentPerformanceFactor> builder)
    {
        builder.ToTable("department_performancefactor");

        builder.HasKey(rpf => rpf.Id);

        builder.Property(rpf => rpf.Weightage)
            .IsRequired()
            .HasColumnType("decimal(5, 2)");

        builder.HasOne(rpf => rpf.Department)
            .WithMany(epf => epf.DepartmentPerformanceFactors)
            .HasForeignKey(rpf => rpf.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(rpf => rpf.PerformanceFactor)
            .WithMany(pf => pf.DepartmentPerformanceFactors)
            .HasForeignKey(rpf => rpf.PerformanceFactorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
