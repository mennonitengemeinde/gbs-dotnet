namespace Gbs.Infrastructure.Persistence.Configurations;

public class GradeConfiguration : IEntityTypeConfiguration<Grade>
{
    public void Configure(EntityTypeBuilder<Grade> builder)
    {
        builder.Property(g => g.GradeType)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(g => g.Date)
            .IsRequired();
        
        builder.Property(g => g.Percent)
            .IsRequired();
        
        builder.Property(g => g.StudentId)
            .IsRequired();
        
        builder.Property(g => g.SubjectId)
            .IsRequired();
    }
}