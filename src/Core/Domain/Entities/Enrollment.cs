namespace Gbs.Core.Domain.Entities;

public class Enrollment
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public int StudentId { get; set; }
    public int GenerationId { get; set; }
    public bool IsActive { get; set; } = false;
    public bool HasCompleted { get; set; } = false;
    public DateOnly EnrollmentDate { get; set; }
    public DateOnly? CompletionDate { get; set; }
    public string Testimony { get; set; } = string.Empty;
    public bool AgreedToGbsConcept { get; set; } = false;
    
    public Student Student { get; set; } = null!;
    public Generation Generation { get; set; } = null!;

    public List<Grade> Grades { get; set; } = new();
}