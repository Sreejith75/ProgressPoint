using Bytestrone.AppraisalSystem.Core.Entities.EmployeeAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class EmployeeAppraiserMappingConfiguration : IEntityTypeConfiguration<EmployeeAppraiserMapping>
{
       public void Configure(EntityTypeBuilder<EmployeeAppraiserMapping> builder)
       {
              builder.ToTable("employee_appraiser_mappings");

              builder.HasKey(am => am.Id);

              // Relationships
              builder.HasOne(am => am.Appraisee)
                     .WithMany(e => e.Appraisees)
                     .HasForeignKey(am => am.EmployeeId)
                     .OnDelete(DeleteBehavior.Cascade);
                     
              builder.Property(am=>am.EmployeeId).HasColumnName("Appraisee_Id");
              builder.Property(am=>am.AppraiserId).HasColumnName("Appraiser_Id");

              builder.HasOne(am => am.Appraiser)
                     .WithMany(e => e.Appraisers)
                     .HasForeignKey(am => am.AppraiserId)
                     .OnDelete(DeleteBehavior.Restrict);

              // Properties
              builder.Property(am => am.EffectiveDate).IsRequired();
              builder.Property(am => am.Status)
                     .IsRequired();
              builder.Property(am => am.ChangedReason)
                     .HasMaxLength(250);
              builder.Property(am => am.EndDate)
                     .IsRequired(false);
       }
}
