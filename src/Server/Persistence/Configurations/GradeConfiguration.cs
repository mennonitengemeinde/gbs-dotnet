namespace Gbs.Server.Persistence.Configurations;

public class GradeConfiguration : IEntityTypeConfiguration<Grade>
{
    public void Configure(EntityTypeBuilder<Grade> builder)
    {
        builder.HasKey(g => new { g.SubjectId, g.EnrollmentId });
    }
}