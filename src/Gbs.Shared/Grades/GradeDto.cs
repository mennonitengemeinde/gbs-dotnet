using Gbs.Shared.Common.Enums;

namespace Gbs.Shared.Grades;

public class GradeDto
{
    public int Id { get; set; }
    public GradeType GradeType { get; set; }
    public DateOnly Date { get; set; }
    public int Percent { get; set; }
    public int StudentId { get; set; }
    public string StudentFirstName { get; set; } = string.Empty;
    public string StudentLastName { get; set; } = string.Empty;
    public int SubjectId { get; set; }
    public string SubjectName { get; set; } = string.Empty;
}