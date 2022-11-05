namespace Gbs.Server.Persistence.Configurations;

public class LiveStreamTeacherConfiguration : IEntityTypeConfiguration<LiveStreamTeacher>
{
    public void Configure(EntityTypeBuilder<LiveStreamTeacher> builder)
    {
        builder.HasKey(lt => new { lt.LiveStreamId, lt.TeacherId });
    }
}