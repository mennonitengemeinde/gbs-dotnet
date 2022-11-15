using Gbs.Application.Common.Entities;

namespace Gbs.Infrastructure.Persistence.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.Property(s => s.MaritalStatus)
            .HasConversion<int>();

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(s => s.UserId);
    }
}