using Bytestrone.AppraisalSystem.Core.Entities.AppraisalFeedbackAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
namespace Bytestrone.AppraisalSystem.Infrastructure.Data.Config;
public class AppraisalFeedbackConfiguration : IEntityTypeConfiguration<AppraisalFeedback>
{
    public void Configure(EntityTypeBuilder<AppraisalFeedback> builder)
    {
        builder.ToTable("appraisal_feedbacks");

        builder.HasKey(af => af.Id);

        

        builder.Property(af => af.EmployeeId)
            .IsRequired();
        builder.HasOne(af => af.Employee).WithMany().HasForeignKey(af => af.EmployeeId).OnDelete(DeleteBehavior.Restrict);

        builder.Property(af=>af.CycleId).IsRequired();
        builder.HasOne(af=>af.AppraisalCycle).WithMany().HasForeignKey(af=>af.CycleId).OnDelete(DeleteBehavior.Restrict);
        

        builder.Property(af => af.AppraiserId)
            .IsRequired();

        builder.Property(af => af.CreatedAt)
            .IsRequired();

        builder.Property(af => af.Status)
            .IsRequired()
            .HasConversion(
                s => s.Name,
                v => FeedbackStatusFromName(v));

        

        builder.HasOne(af => af.AppraisalSummary)
            .WithMany()
            .HasForeignKey(af => af.SummaryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Navigation(af => af.FeedbackDetails)
            .UsePropertyAccessMode(PropertyAccessMode.Field);


        builder.HasIndex(af => af.EmployeeId);
        builder.HasIndex(af => af.AppraiserId);
        builder.HasIndex(af => af.SummaryId);
    }
    private static FeedbackStatus FeedbackStatusFromName(string name)
    {
        return FeedbackStatus.FromName(name);
    }
}
