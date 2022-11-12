using System.ComponentModel.DataAnnotations;

namespace Gbs.Shared.Dto.Generations;

public class GenerationCreateDto
{
    [Required, MinLength(3, ErrorMessage = "Name must be at least 3 characters long")]
    public string Name { get; set; } = string.Empty;
}