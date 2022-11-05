using System.ComponentModel.DataAnnotations.Schema;

namespace Gbs.Core.Domain.Entities;

public class Question
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    [Column(TypeName = "timestamp without time zone")]
    public DateTime CreatedAt { get; set; }
    [Column(TypeName = "timestamp without time zone")]
    public DateTime ClosedAt { get; set; }
    public string Timezone { get; set; } = string.Empty;
    public bool IsClosed { get; set; } = false;

    public string UserId { get; set; } = string.Empty;
    // public User User { get; set; } = null!;

    public List<Message> Messages { get; set; } = new List<Message>();
}