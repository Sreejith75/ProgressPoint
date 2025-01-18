using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bytestrone.AppraisalSystem.Core.Entities.DepartmentAggregate;
namespace Bytestrone.AppraisalSystem.Infrastructure.Data.Config;
public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("department");
        builder.HasKey(q => q.Id);

        builder.Property(q => q.DepartmentName)
            .IsRequired();

    }
}
