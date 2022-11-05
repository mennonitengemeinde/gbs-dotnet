namespace Gbs.Server.Persistence.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.Property(s => s.MaritalStatus)
            .HasConversion<int>();
    }
}