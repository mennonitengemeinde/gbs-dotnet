using System.ComponentModel.DataAnnotations;

namespace Gbs.Domain.Subjects;

public class SubjectCreateDto
{
    [Required]
    [MinLength(3, ErrorMessage = "Subject name must be at least 3 characters long")]
    public string Name { get; set; } = string.Empty;
}