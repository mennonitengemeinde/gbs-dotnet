namespace Gbs.Server.Persistence.Configurations;

public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {
        builder.HasOne<User>()
            .WithOne(u => u.Teacher)
            .HasForeignKey<User>(u => u.TeacherId);
    }
}