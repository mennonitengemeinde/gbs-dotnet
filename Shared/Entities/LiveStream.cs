using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gbs.Shared.Entities;

public class LiveStream
{
    public int Id { get; set; }
    [Required, StringLength(150, MinimumLength = 3)]
    public string Title { get; set; } = string.Empty;
    [Required, Url]
    public string Url { get; set; } = string.Empty;
    public bool IsLive { get; set; } = false;
    
    [Required]
    public int GenerationId { get; set; }
    public Generation Generation { get; set; } = null!;

    [Required, MinLength(1)]
    public List<Teacher> Teachers { get; set; } = new List<Teacher>();
}