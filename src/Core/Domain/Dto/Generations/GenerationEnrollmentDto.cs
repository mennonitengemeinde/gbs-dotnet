namespace Gbs.Core.Domain.Dto.Generations;

public class GenerationEnrollmentDto
{
    public string Id { get; set; } = string.Empty;
    public int StudentId { get; set; }
    public bool IsActive { get; set; }
    public bool HasCompleted { get; set; }
    public DateOnly EnrollmentDate { get; set; }
    public DateOnly? CompletionDate { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public MaritalStatus MaritalStatus { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public int ChurchId { get; set; }
    public string ChurchName { get; set; } = string.Empty;
}