using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalFormAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.QuestionAggregate;

namespace Bytestrone.AppraisalSystem.Infrastructure.Data.Config;
public class FormQuestionConfiguration : IEntityTypeConfiguration<FormQuestion>
{
    public void Configure(EntityTypeBuilder<FormQuestion> builder)
    {
        builder.HasKey(fq => new { fq.AppraisalFormId, fq.QuestionId });

        builder.HasOne(fq => fq.AppraisalForm)
               .WithMany(af => af.FormQuestionMappings) 
               .HasForeignKey(fq => fq.AppraisalFormId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(fq => fq.Question)
               .WithMany() 
               .HasForeignKey(fq => fq.QuestionId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("appraisal_form_question");

    }
}
