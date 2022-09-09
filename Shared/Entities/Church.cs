﻿namespace gbs.Shared.Entities;

public class Church
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? PostalCode { get; set; }

    public List<Student> Students { get; set; } = new List<Student>();
}