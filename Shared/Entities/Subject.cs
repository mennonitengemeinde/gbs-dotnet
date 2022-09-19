﻿namespace gbs.Shared.Entities;

public class Subject
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public List<Lesson> Lessons { get; set; } = new List<Lesson>();
    public List<Question> Questions { get; set; } = new List<Question>();
}