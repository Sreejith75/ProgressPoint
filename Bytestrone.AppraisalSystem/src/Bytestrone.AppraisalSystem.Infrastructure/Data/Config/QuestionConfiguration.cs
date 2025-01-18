using Bytestrone.AppraisalSystem.Core.ContributorAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.QuestionAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.ToTable("question");
        builder.HasKey(q => q.Id);

        builder.Property(q => q.QuestionText)
            .IsRequired()
            .HasMaxLength(500);

        

        builder.Property(x => x.Status)
            .HasConversion(
                x => x.Name,
                x => QuestionStatusFromName(x));
    }

    private static QuestionStatus QuestionStatusFromName(string name)
    {
        return QuestionStatus.FromName(name);
    }
}
