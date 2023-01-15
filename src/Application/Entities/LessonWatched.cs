namespace Gbs.Application.Entities;

public class LessonWatched
{
    public int LessonId { get; set; }
    public Lesson Lesson { get; set; } = null!;

    public string UserId { get; set; } = string.Empty;
    public User User { get; set; } = null!;
    
    public DateTime WatchedAt { get; set; }
}