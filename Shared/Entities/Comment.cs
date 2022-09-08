namespace gbs.Shared.Entities;

public class Comment
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public bool IsAnswer { get; set; } = false;

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int QuestionId { get; set; }
    public Question Question { get; set; } = null!;
}