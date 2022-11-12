﻿using Gbs.Shared.Dto.Students;

namespace Gbs.Shared.Dto.Generations;

public class GenerationDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    public IEnumerable<StudentEnrollmentDto> Enrollments { get; set; } = null!;
}