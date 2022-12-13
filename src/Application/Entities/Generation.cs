namespace Gbs.Application.Entities;

public class Generation
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    public List<Student> Students { get; set; } = new List<Student>();
    public List<Lesson> Lessons { get; set; } = new List<Lesson>();
}