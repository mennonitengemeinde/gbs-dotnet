namespace Gbs.Core.Domain.Dto.Students;

public class StudentEnrollmentDto
{
    public string Id { get; set; } = string.Empty;
    public int GenerationId { get; set; }
    public string GenerationName { get; set; } = string.Empty;
    public bool IsActive { get; set; } = false;
    public bool HasCompleted { get; set; } = false;
    public DateOnly EnrollmentDate { get; set; }
    public DateOnly CompletionDate { get; set; }
    public string Testimony { get; set; } = string.Empty;
    public bool AgreedToGbsConcept { get; set; } = false;
}