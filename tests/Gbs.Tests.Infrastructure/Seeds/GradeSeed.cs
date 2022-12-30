using Gbs.Application.Entities;

namespace Gbs.Tests.Infrastructure.Seeds;

public static class GradeSeed
{
    public static List<Grade> GetGrades()
    {
        return new List<Grade>
        {
            new Grade
            {
                Id = 1,
                SubjectId = 1,
                StudentId = 1,
                GradeTypeId = 1,
                Date = new DateOnly(2021, 01, 01),
                Percent = 90,
            },
            new Grade
            {
                Id = 2,
                SubjectId = 1,
                StudentId = 1,
                GradeTypeId = 1,
                Date = new DateOnly(2021, 01, 01),
                Percent = 80
            },
            new Grade
            {
                Id = 3,
                SubjectId = 1,
                StudentId = 1,
                GradeTypeId = 1,
                Date = new DateOnly(2021, 01, 01),
                Percent = 70
            }
        };
    }
}