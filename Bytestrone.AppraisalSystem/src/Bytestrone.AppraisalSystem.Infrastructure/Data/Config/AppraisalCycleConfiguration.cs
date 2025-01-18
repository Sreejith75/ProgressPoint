using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalCycleAggregate;

namespace Bytestrone.AppraisalSystem.Infrastructure.Data.Config;
public class AppraisalCycleConfiguration : IEntityTypeConfiguration<AppraisalCycle>
{
    public void Configure(EntityTypeBuilder<AppraisalCycle> builder)
    {
        builder.HasKey(ac => ac.Id);

        builder.ToTable("appraisal_cycle");

        builder.Property(ac => ac.Quarter)
            .HasConversion(
                q => q.Name,  
                v => QuarterFromName(v)) 
            .IsRequired();

        builder.Property(ac => ac.Status)
            .HasConversion(
                s => s.Name, 
                v => CycleStatusFromName(v)) 
            .IsRequired();

        builder.Property(ac => ac.Year)
            .IsRequired();

        builder.OwnsOne(ac => ac.AppraiseeDateRange, dr =>
        {
            dr.Property(d => d.StartDate).IsRequired().HasColumnName("AppraiseeDateRange_StartDate");
            dr.Property(d => d.EndDate).IsRequired().HasColumnName("AppraiseeDateRange_EndDate");
        });

        builder.OwnsOne(ac => ac.AppraiserDateRange, dr =>
        {
            dr.Property(d => d.StartDate).IsRequired().HasColumnName("AppraiserDateRange_StartDate");
            dr.Property(d => d.EndDate).IsRequired().HasColumnName("AppraiserDateRange_EndDate");
        });
    }

    private static Quarter QuarterFromName(string name)
    {
        return Quarter.FromName(name);
    }
    private static CycleStatus CycleStatusFromName(string name)
    {
        return CycleStatus.FromName(name);
    }
}
