namespace Gbs.Core.Domain.Dto.Grades;

public class GradeDto
{
    public int EnrollmentId { get; set; }
    public int SubjectId { get; set; }
    public string SubjectName { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public int Percent { get; set; }
}