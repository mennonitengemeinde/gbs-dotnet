using Gbs.Shared.Common.Enums;

namespace Gbs.Shared.Generations;

public class GenerationLessonDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? VideoUrl { get; set; }
    public int Order { get; set; }
    public Visibility IsVisible { get; set; }
    public int TeacherId { get; set; }
    public string Teacher { get; set; } = string.Empty;
}