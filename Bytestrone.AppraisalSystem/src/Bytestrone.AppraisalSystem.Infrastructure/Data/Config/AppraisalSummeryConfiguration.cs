using Bytestrone.AppraisalSystem.Core.Entities.AppraisalSummeryAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Identity.Client;

namespace Bytestrone.AppraisalSystem.Infrastructure.Data.Config;
public class AppraisalSummaryConfiguration : IEntityTypeConfiguration<AppraisalSummary>
{
    public void Configure(EntityTypeBuilder<AppraisalSummary> builder)
    {
        builder.ToTable("appraisal_summaries");

        builder.HasKey(asummary => asummary.Id);
        

        builder.Property(asummary => asummary.EmployeeId)
            .IsRequired();

        builder.Property(asummary => asummary.CycleId)
            .IsRequired();

        builder.Property(asummary => asummary.AppraiseeScore)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(asummary => asummary.AppraiserScore)
            .HasColumnType("decimal(18,2)");

        builder.Property(asummary => asummary.PerformanceBucket)
            .HasConversion(
                p => p.Name,
                p => PerformanceBucketFromName(p));

        builder.HasOne(asummary => asummary.Employee)
            .WithMany(asummary=>asummary.AppraisalSummaries)
            .HasForeignKey(asummary => asummary.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);
        

        builder.HasOne(asummary => asummary.AppraisalCycle)
            .WithMany()
            .HasForeignKey(asummary => asummary.CycleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(asummary => asummary.EmployeeId);
        builder.HasIndex(asummary => asummary.CycleId);
    }

    private static PerformanceBucket PerformanceBucketFromName(string name)
    {
        return PerformanceBucket.FromName(name);
    }
}

