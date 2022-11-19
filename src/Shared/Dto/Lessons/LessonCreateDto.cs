namespace Gbs.Shared.Dto.Lessons;

public class LessonCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string? VideoUrl { get; set; }
    public int Order { get; set; }
    public Visibility IsVisible { get; set; }
    public int GenerationId { get; set; }
    public int SubjectId { get; set; }
    public int TeacherId { get; set; }
}