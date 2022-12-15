using Gbs.Application.Entities;

namespace Gbs.Infrastructure.Persistence.Configurations;

public class GradeTypeConfiguration : IEntityTypeConfiguration<GradeType>
{
    public void Configure(EntityTypeBuilder<GradeType> builder)
    {
        builder.HasOne(gt => gt.Generation)
            .WithMany(g => g.GradeTypes)
            .HasForeignKey(gt => gt.GenerationId)
            .IsRequired();
    }
}