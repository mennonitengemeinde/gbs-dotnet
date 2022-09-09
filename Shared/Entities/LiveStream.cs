using System.ComponentModel.DataAnnotations.Schema;

namespace gbs.Shared.Entities;

public class LiveStream
{
    public int Id { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime Start { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime End { get; set; }

    public string TimeZoneId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public bool IsLive { get; set; } = false;

    public List<Teacher> Teachers { get; set; } = new List<Teacher>();
    public List<Generation> Generations { get; set; } = new List<Generation>();
}