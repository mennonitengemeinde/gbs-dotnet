using Gbs.Shared.Common.Enums;

namespace Gbs.Domain.Entities;

public class Grade
{
    public int Id { get; set; }
    public GradeType GradeType { get; set; }
    public DateOnly Date { get; set; }
    public int Percent { get; set; }
    
    public int StudentId { get; set; }
    public Student Student { get; set; } = null!;
    
    public int SubjectId { get; set; }
    public Subject Subject { get; set; } = null!;
}