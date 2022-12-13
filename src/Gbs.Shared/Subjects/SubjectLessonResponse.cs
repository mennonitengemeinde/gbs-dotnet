using Gbs.Shared.Common.Enums;

namespace Gbs.Shared.Subjects;

public class SubjectLessonResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? VideoUrl { get; set; }
    public int Order { get; set; }
    public Visibility IsVisible { get; set; }
    public int GenerationId { get; set; }
    public string GenerationName { get; set; } = string.Empty;
    public int TeacherId { get; set; }
    public string TeacherName { get; set; } = string.Empty;
}