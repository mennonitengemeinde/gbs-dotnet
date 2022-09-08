namespace gbs.Shared.Entities;

public class Question
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public bool IsClosed { get; set; } = false;

    public int UserId { get; set; }
    public User User { get; set; } = null!;
}