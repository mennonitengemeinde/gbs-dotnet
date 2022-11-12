namespace Gbs.Domain.Entities;

public class Grade
{
    public int EnrollmentId { get; set; }
    public int SubjectId { get; set; }
    public DateOnly Date { get; set; }
    public int Percent { get; set; }
    
    public Subject Subject { get; set; } = null!;
}