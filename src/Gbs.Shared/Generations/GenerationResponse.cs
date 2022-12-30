using Gbs.Shared.Students;

namespace Gbs.Shared.Generations;

public class GenerationResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    public IEnumerable<GenerationStudentResponse> Students { get; set; } = new List<GenerationStudentResponse>();
}