using Gbs.Shared.Common.Enums;

namespace Gbs.Shared.Lessons;

public class LessonResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? VideoUrl { get; set; }
    public int Order { get; set; }
    public Visibility IsVisible { get; set; }
    public int GenerationId { get; set; }
    public string GenerationName { get; set; } = string.Empty;
    public int? SubjectId { get; set; }
    public string? SubjectName { get; set; } = string.Empty;
    public int TeacherId { get; set; }
    public string TeacherName { get; set; } = string.Empty;
    public bool HasWatched { get; set; }
}