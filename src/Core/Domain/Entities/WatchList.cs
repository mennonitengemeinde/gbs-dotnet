namespace Gbs.Core.Domain.Entities;

public class WatchList
{
    public string UserId { get; set; } = string.Empty;
    public int QuestionId { get; set; }
    public bool IsAnswered { get; set; } = false;
}