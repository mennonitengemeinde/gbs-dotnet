namespace Gbs.Tests.Infrastructure.Seeds;

public static class GradeSeed
{
    public static List<Grade> GetGrades()
    {
        return new List<Grade>
        {
            new Grade
            {
                SubjectId = 1, EnrollmentId = 1,
                Date = new DateOnly(2021, 01, 01), Percent = 90
            },
            new Grade
            {
                SubjectId = 2, EnrollmentId = 1,
                Date = new DateOnly(2021, 01, 01), Percent = 80
            },
            new Grade
            {
                SubjectId = 3, EnrollmentId = 1,
                Date = new DateOnly(2021, 01, 01), Percent = 70
            }
        };
    }
}