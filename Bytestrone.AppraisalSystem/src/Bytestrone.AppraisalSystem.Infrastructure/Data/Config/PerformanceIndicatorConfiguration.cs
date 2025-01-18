using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bytestrone.AppraisalSystem.Core.Entities.PerformanceIndicatorAggregate;

namespace Bytestrone.AppraisalSystem.Infrastructure.Data.Config;

public class PerformanceIndicatorConfiguration : IEntityTypeConfiguration<PerformanceIndicator>
{
    public void Configure(EntityTypeBuilder<PerformanceIndicator> builder)
    {
        builder.ToTable("performance_indicator");

        builder.HasKey(pi => pi.Id);

        builder.Property(pi => pi.IndicatorName)
            .IsRequired()
            .HasMaxLength(100)
            .HasDefaultValue("Default Indicator");

        builder.Property(pi => pi.FactorId)
            .IsRequired();

        builder.Property(pi => pi.Weightage)
            .IsRequired()
            .HasDefaultValue(0);

        // Configure relationship with PerformanceFactor
        builder.HasOne(pi => pi.PerformanceFactor)
            .WithMany(pf => pf.Indicators)
            .HasForeignKey(pi => pi.FactorId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure relationship with Questions
        builder.HasMany(pi => pi.Questions)
            .WithOne(q => q.Indicator)
            .HasForeignKey(q => q.PerformanceIndicatorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
