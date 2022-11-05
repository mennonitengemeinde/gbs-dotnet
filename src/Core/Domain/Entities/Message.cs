using System.ComponentModel.DataAnnotations.Schema;

namespace Gbs.Core.Domain.Entities;

public class Message
{
    public int Id { get; set; }
    public MessageType MessageType { get; set; }
    public string Text { get; set; } = string.Empty;
    [Column(TypeName = "timestamp without time zone")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string Timezone { get; set; } = string.Empty;

    public int QuestionId { get; set; }
    public Question Question { get; set; } = null!;
    
    public string UserId { get; set; } = string.Empty;
    // public User User { get; set; } = null!;
}