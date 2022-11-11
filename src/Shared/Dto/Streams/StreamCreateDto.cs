using System.ComponentModel.DataAnnotations;

namespace Gbs.Shared.Dto.Streams;

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
    public IEnumerable<int> Teachers { get; set; } = new HashSet<int>();
}