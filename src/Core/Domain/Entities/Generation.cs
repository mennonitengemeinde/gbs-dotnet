namespace Gbs.Core.Domain.Entities;

public class Generation
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}