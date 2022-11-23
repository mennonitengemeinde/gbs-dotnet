namespace Gbs.Domain.Entities;

public class Teacher
{
    public int Id { get; set; }

    // [Required]
    public string Name { get; set; } = string.Empty;
    public string? UserId { get; set; }

    public List<Lesson> Lessons { get; set; } = new();
    // public User? User { get; set; }

    // public override string ToString()
    // {
    //     return Name;
    // }
    //
    // public override int GetHashCode() => Id.GetHashCode();
    //
    // public override bool Equals(object? obj)
    // {
    //     if (obj is Teacher teacher)
    //     {
    //         return teacher.Id == Id;
    //     }
    //
    //     return false;
    // }
}