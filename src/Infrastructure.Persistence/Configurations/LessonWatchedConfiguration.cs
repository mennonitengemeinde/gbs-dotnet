using Gbs.Application.Entities;

namespace Gbs.Infrastructure.Persistence.Configurations;

public class LessonWatchedConfiguration : IEntityTypeConfiguration<LessonWatched>
{
    public void Configure(EntityTypeBuilder<LessonWatched> builder)
    {
        builder.HasKey(x => new { x.LessonId, x.UserId });
        
        builder.HasOne(x => x.Lesson)
            .WithMany(x => x.UsersWatched)
            .HasForeignKey(x => x.LessonId);
        
        builder.HasOne(x => x.User)
            .WithMany(u => u.LessonsWatched)
            .HasForeignKey(x => x.UserId);

        builder.Property(x => x.WatchedAt)
            .IsRequired();
    }
}