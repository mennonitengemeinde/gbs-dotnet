namespace Gbs.Domain.Entities;

public class GenerationLesson
{
    public int GenerationId { get; set; }
    public int LessonId { get; set; }
    public int Order { get; set; }
    public string VideoUrl { get; set; } = string.Empty;

    public Generation Generation { get; set; } = null!;
    public Lesson Lesson { get; set; } = null!;
}