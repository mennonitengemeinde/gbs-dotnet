using Gbs.Core.Domain.Entities;

namespace Gbs.Core.Domain.Dto.Generations;

public class GenerationDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}