using System.ComponentModel.DataAnnotations;

namespace Gbs.Core.Domain.Entities;

public class LiveStreamTeacher
{
    [Required]
    public int LiveStreamId { get; set; }
    public LiveStream LiveStream { get; set; } = null!;
    
    [Required]
    public int TeacherId { get; set; }
    public Teacher Teacher { get; set; } = null!;
}