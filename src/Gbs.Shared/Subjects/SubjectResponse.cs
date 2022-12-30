namespace Gbs.Shared.Subjects;

public class SubjectResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public List<SubjectLessonResponse> Lessons { get; set; } = new();
}