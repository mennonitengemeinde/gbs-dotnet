﻿namespace gbs.Shared.Entities;

public class Teacher
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public int? UserId { get; set; }
    public User? User { get; set; }
}