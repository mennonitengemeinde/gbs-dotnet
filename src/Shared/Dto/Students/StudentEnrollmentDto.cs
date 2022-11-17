﻿namespace Gbs.Shared.Dto.Students;

public class StudentEnrollmentDto
{
    public int Id { get; set; }
    public int GenerationId { get; set; }
    public string GenerationName { get; set; } = string.Empty;
    public bool IsActive { get; set; } = false;
    public bool HasCompleted { get; set; } = false;
    public DateOnly EnrollmentDate { get; set; }
    public DateOnly? CompletionDate { get; set; }
    public string Testimony { get; set; } = string.Empty;
    public bool AgreedToGbsConcept { get; set; } = false;
}