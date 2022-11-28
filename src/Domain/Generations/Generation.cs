namespace Gbs.Domain.Generations;

public class Generation
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    public List<Lesson> Lessons { get; set; } = new List<Lesson>();
}