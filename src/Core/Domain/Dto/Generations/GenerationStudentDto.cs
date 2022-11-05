namespace Gbs.Core.Domain.Dto.Generations;

public class GenerationStudentDto
{
    public string Name { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public MaritalStatus MaritalStatus { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public int ChurchId { get; set; }
    public string ChurchName { get; set; } = string.Empty;
    public bool EnrollmentId { get; set; }

    public IEnumerable<GradeDto> Grades { get; set; } = new List<GradeDto>();
}