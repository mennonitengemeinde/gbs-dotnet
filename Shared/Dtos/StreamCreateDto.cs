using System.ComponentModel.DataAnnotations;
using gbs.Shared.Entities;

namespace gbs.Shared.Dtos;

public class StreamCreateDto
{
    [Required]
    public string Title { get; set; } = string.Empty;
    [Required]
    public string Url { get; set; } = string.Empty;
    public bool IsLive { get; set; } = false;
    
    [Required]
    public int GenerationId { get; set; }

    [Required, MinLength(1)]
    public List<int> Teachers { get; set; } = new List<int>();
}