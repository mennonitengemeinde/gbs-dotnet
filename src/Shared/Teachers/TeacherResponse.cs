namespace Gbs.Shared.Teachers;

public class TeacherResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public string? UserId { get; set; }

    public override string ToString()
    {
        return Name;
    }

    public override int GetHashCode() => Id.GetHashCode();

    public override bool Equals(object? obj)
    {
        if (obj is TeacherResponse teacher)
        {
            return teacher.Id == Id;
        }

        return false;
    }
}