using Gbs.Application.Entities;

namespace Gbs.Tests.Infrastructure.Seeds;

public static class TeacherSeed
{
    public static List<Teacher> GetTeachers()
    {
        return new List<Teacher>
        {
            new Teacher
            {
                Id = 1,
                Name = "Teacher 1"
            },
            new Teacher
            {
                Id = 2,
                Name = "Teacher 2"
            },
            new Teacher
            {
                Id = 3,
                Name = "Teacher 3"
            }
        };
    }
}