using gbs.Shared.Entities;

namespace gbs.Shared.Dtos.LiveStream;

public class CreateStreamDto
{
    public string Title { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public bool IsLive { get; set; } = false;
    
    public int GenerationId { get; set; }

    public List<Teacher> Teachers { get; set; } = new List<Teacher>();
}