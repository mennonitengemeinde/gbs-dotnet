using Gbs.Domain.Entities;

namespace Gbs.Infrastructure.Persistence.Configurations;

public class SubjectDocumentConfiguration : IEntityTypeConfiguration<SubjectDocument>
{
    public void Configure(EntityTypeBuilder<SubjectDocument> builder)
    {
        builder.Property(sd => sd.SubjectDocumentType)
            .HasConversion<int>();
    }
}