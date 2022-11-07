namespace Gbs.Core.Domain.Dto.Enrollments;

public class EnrollmentDto
{
    public int StudentId { get; set; }
    public int GenerationId { get; set; }
    
    
    public StudentDto Student { get; set; } = null!;
    public GenerationDto Generation { get; set; } = null!;

    public List<GradeDto> Grades { get; set; } = new();
}