using Bytestrone.AppraisalSystem.Core.Entities.AppraisalFeedbackAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bytestrone.AppraisalSystem.Infrastructure.Data.Config;
public class AppraisalFeedbackDetailConfiguration : IEntityTypeConfiguration<AppraisalFeedbackDetail>
{
    public void Configure(EntityTypeBuilder<AppraisalFeedbackDetail> builder)
    {
        builder.ToTable("appraisal_feedback_details");

        builder.HasKey(afd => afd.Id);

        builder.HasOne(afd => afd.AppraisalFeedback)
            .WithMany(af => af.FeedbackDetails)
            .HasForeignKey(afd => afd.FeedbackId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(afd => afd.FeedbackId)
            .IsRequired();

        builder.Property(afd => afd.QuestionId)
            .IsRequired();

        builder.HasOne(afd => afd.Question)
                .WithMany()
                .HasForeignKey(afd => afd.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.Property(afd => afd.AppraiseeRating)
            .IsRequired();

        builder.Property(afd => afd.AppraiseeComment)
            .IsRequired();

        builder.Property(afd => afd.AppraiserRating)
            .IsRequired(false);

        builder.Property(afd => afd.AppraiserComment)
            .IsRequired(false);

        builder.Property(afd => afd.Status)
            .IsRequired()
            .HasConversion(
                s => s!.Name,
                v => FeedbackStatusFromName(v));

        builder.OwnsOne(afd => afd.Artifact, artifact =>
        {
            artifact.Property(a => a.FilePath)
                .HasColumnName("Artifact_FilePath");

            artifact.Property(a => a.FileType)
                .HasColumnName("Artifact_FileType");
        });

        builder.HasIndex(afd => afd.FeedbackId);
        builder.HasIndex(afd => afd.QuestionId);
    }

    private static FeedbackStatus FeedbackStatusFromName(string name)
    {
        return FeedbackStatus.FromName(name);
    }
}
