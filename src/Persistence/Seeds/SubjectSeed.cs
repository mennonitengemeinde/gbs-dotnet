namespace Gbs.Infrastructure.Persistence.Seeds;

public static class SubjectSeed
{
    public static List<Subject> GetSubjects()
    {
        return new List<Subject>
        {
            new Subject { Id = 1, Name = "Subject 1" },
            new Subject { Id = 2, Name = "Subject 2" },
            new Subject { Id = 3, Name = "Subject 3" }
        };
    }
}