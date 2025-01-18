using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalFormAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.QuestionAggregate;
using Bytestrone.AppraisalSystem.Core.Entities.AppraisalCycleAggregate;

namespace Bytestrone.AppraisalSystem.Infrastructure.Data.Config;
public class AppraisalFormConfiguration : IEntityTypeConfiguration<AppraisalForm>
{
    public void Configure(EntityTypeBuilder<AppraisalForm> builder)
    {
        builder.HasKey(af => af.Id);

        builder.ToTable("appraisal_form");

        builder.Property(af => af.Status)
            .HasConversion(
                v => v.Name,
                v => FormStatusFromName(v))
            .IsRequired();

        builder.Property(af => af.EmployeeRoleId)
            .IsRequired();

        builder.Property(af=>af.CreatedAt).HasColumnName("CreatedAt");

        builder.HasOne(af => af.EmployeeRole)
            .WithMany()
            .HasForeignKey(af => af.EmployeeRoleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(af => af.FormQuestionMappings)
            .WithOne()
            .HasForeignKey(fq => fq.AppraisalFormId)
            .OnDelete(DeleteBehavior.Cascade);
    }
    private static FormStatus FormStatusFromName(string name)
    {
        return FormStatus.FromName(name);
    }
}

