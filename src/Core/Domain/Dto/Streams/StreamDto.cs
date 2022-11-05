namespace Gbs.Core.Domain.Dto.Streams;

public class StreamDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public bool IsLive { get; set; } = false;
    
    public int GenerationId { get; set; }
    public string GenerationName { get; set; } = string.Empty;

    public IEnumerable<TeacherDto> Teachers { get; set; } = new List<TeacherDto>();
}