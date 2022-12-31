namespace Gbs.Shared.Streams;

public interface IStreamRequest
{
    public string Title { get; set; }
    public string Url { get; set; }
    public bool IsLive { get; set; }
    public int GenerationId { get; set; }
    public IEnumerable<int> Teachers { get; set; }
}