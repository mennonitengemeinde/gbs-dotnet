namespace Gbs.Application.Entities;

public class GradeType
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public int GenerationId { get; set; }
    public Generation Generation { get; set; } = null!;

    public ICollection<Grade> Grades { get; set; } = new List<Grade>();
}