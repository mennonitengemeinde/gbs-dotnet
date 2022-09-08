namespace gbs.Shared.Entities;

public class Stream
{
    public int Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public bool IsLive { get; set; } = false;

    public List<Teacher> Teachers { get; set; } = new List<Teacher>();
}