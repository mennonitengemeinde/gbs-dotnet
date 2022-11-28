namespace Gbs.Domain.Subjects;

public class SubjectDocument
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public SubjectDocumentType SubjectDocumentType { get; set; }
    public bool IsActive { get; set; } = true;

    public int SubjectId { get; set; }
    public Subject Subject { get; set; } = null!;
}