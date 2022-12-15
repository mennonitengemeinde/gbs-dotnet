using Gbs.Application.Entities;

namespace Gbs.Infrastructure.Persistence.Configurations;

public class GradeConfiguration : IEntityTypeConfiguration<Grade>
{
    public void Configure(EntityTypeBuilder<Grade> builder)
    {
        builder.Property(g => g.Date)
            .IsRequired();

        builder.Property(g => g.Percent)
            .IsRequired();
        
        builder.HasOne(g => g.GradeType)
            .WithMany(gt => gt.Grades)
            .HasForeignKey(g => g.GradeTypeId)
            .IsRequired();

        builder.HasOne(g => g.Student)
            .WithMany(s => s.Grades)
            .HasForeignKey(g => g.StudentId)
            .IsRequired();

        builder.HasOne(g => g.Subject)
            .WithMany(g => g.Grades)
            .HasForeignKey(g => g.SubjectId)
            .IsRequired();
    }
}