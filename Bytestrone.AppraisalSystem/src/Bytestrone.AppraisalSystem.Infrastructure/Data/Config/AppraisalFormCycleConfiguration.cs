// using Bytestrone.AppraisalSystem.Core.Entities.AppraisalFormAggregate;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
// using Org.BouncyCastle.Math.EC.Rfc7748;

// namespace Bytestrone.AppraisalSystem.Infrastructure.Data.Config;
// public class AppraisalFormCycleConfiguration : IEntityTypeConfiguration<AppraisalFormCycle>
// {
//   public void Configure(EntityTypeBuilder<AppraisalFormCycle> builder)
//   {
//     builder.HasKey(fc=>new {fc.FormId,fc.CycleId});
//     builder.ToTable("appraisalform_cycle");
//     builder.Property(fc=>fc.CycleId).HasColumnName("cycle_id");
//     builder.HasOne(fc=>fc.AppraisalCycle)
//            .WithMany()
//            .HasForeignKey(fc=>fc.CycleId);
//     builder.HasOne(fc=>fc.AppraisalForm)
//             .WithMany()
//             .HasForeignKey(fc=>fc.FormId);

//     builder.Property(fc=>fc.FormId).HasColumnName("form_id");
//   }
// }