using Gbs.Application.Entities;

namespace Gbs.Infrastructure.Persistence.Configurations;

public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {
        builder.HasOne<User>()
            .WithOne()
            .HasForeignKey<Teacher>(t => t.UserId);
    }
}