using System.ComponentModel.DataAnnotations;

namespace Gbs.Shared.Dto.Lessons;

public class LessonCreateDto
{
    [Required]
    [MinLength(3, ErrorMessage = "Name must be at least 3 characters long")]
    public string Name { get; set; } = string.Empty;

    public string? VideoUrl { get; set; }
    [Required] public Visibility IsVisible { get; set; }
    [Required] public int GenerationId { get; set; }
    [Required] public int SubjectId { get; set; }
    [Required] public int TeacherId { get; set; }
}