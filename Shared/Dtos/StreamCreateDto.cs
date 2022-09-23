using System.ComponentModel.DataAnnotations;
using gbs.Shared.Entities;

namespace gbs.Shared.Dtos;

public class StreamCreateDto
{
    [Required]
    public string Title { get; set; } = string.Empty;
    [Required, Url]
    public string Url { get; set; } = string.Empty;
    public bool IsLive { get; set; } = false;
    
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Please select a generation")]
    public int? GenerationId { get; set; }

    [Required, MinLength(1, ErrorMessage = "Please select at least one teacher")]
    public List<int> Teachers { get; set; } = new List<int>();
}