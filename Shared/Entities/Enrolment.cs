namespace gbs.Shared.Entities;

public class Enrolment
{
    public int StudentId { get; set; }
    public int GenerationId { get; set; }
    public bool IsCurrent { get; set; } = false;
    public bool HasCompleted { get; set; } = false;
    
    public Student Student { get; set; } = null!;
}