namespace gbs.Shared.Entities;

public class WatchList
{
    public int UserId { get; set; }
    public int QuestionId { get; set; }
    public bool IsAnswered { get; set; } = false;
}