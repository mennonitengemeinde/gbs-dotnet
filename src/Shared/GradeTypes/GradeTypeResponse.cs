namespace Gbs.Shared.GradeTypes;

public class GradeTypeResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int GenerationId { get; set; }
    public string GenerationName { get; set; } = string.Empty;
}