﻿namespace Gbs.Domain.Generations;

public class GenerationDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    public IEnumerable<StudentDto> Students { get; set; } = new List<StudentDto>();
}