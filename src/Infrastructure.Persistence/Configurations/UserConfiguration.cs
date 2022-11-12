namespace Gbs.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasOne(u => u.Teacher)
            .WithOne()
            .HasForeignKey<User>(u => u.TeacherId);
    }
}