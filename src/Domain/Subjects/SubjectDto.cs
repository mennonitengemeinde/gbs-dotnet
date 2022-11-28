namespace Gbs.Domain.Subjects;

public class SubjectDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public List<SubjectLessonDto> Lessons { get; set; } = new();
}