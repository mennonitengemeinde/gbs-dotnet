namespace Gbs.Server.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasOne(t => t.Teacher)
            .WithOne()
            .HasForeignKey<Teacher>(t => t.UserId);
    }
}