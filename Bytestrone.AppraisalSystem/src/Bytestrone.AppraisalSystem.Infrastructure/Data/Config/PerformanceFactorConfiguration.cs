using Bytestrone.AppraisalSystem.Core.Entities.PerformanceFactorAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Bytestrone.AppraisalSystem.Infrastructure.Data.Config;
public class PerformanceFactorConfiguration : IEntityTypeConfiguration<PerformanceFactor>
{
    public void Configure(EntityTypeBuilder<PerformanceFactor> builder)
    {
        builder.ToTable("performance_factor");
        builder.HasKey(pf => pf.Id);

        builder.Property(pf => pf.FactorName)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(pf => pf.DepartmentPerformanceFactors)
             .WithOne(rpf => rpf.PerformanceFactor)
             .HasForeignKey(rpf => rpf.PerformanceFactorId)
             .OnDelete(DeleteBehavior.Cascade);

        // Ignore Indicators for now if it's not directly persisted
        builder.Ignore(pf => pf.Indicators);
    }
}
