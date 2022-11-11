namespace Gbs.Domain.Entities;

public class Lesson
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public int SubjectId { get; set; }
    public Subject Subject { get; set; } = null!;

    public int TeacherId { get; set; }
    public Teacher Teacher { get; set; } = null!;
}