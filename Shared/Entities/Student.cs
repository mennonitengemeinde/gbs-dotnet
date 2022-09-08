namespace gbs.Shared.Entities;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public int ChurchId { get; set; }
    public Church Church { get; set; } = null!;

    public int? UserId { get; set; }
    public User? User { get; set; }
}