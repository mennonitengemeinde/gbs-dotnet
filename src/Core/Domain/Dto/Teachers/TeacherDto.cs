using System.ComponentModel.DataAnnotations;

namespace Gbs.Core.Domain.Dto.Teachers;

public class TeacherDto
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;

    public string? UserId { get; set; }
    // public User? User { get; set; }

    public override string ToString()
    {
        return Name;
    }

    public override int GetHashCode() => Id.GetHashCode();

    public override bool Equals(object? obj)
    {
        if (obj is TeacherDto teacher)
        {
            return teacher.Id == Id;
        }

        return false;
    }
}