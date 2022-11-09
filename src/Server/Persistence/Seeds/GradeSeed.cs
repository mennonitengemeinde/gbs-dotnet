namespace Gbs.Server.Persistence.Seeds;

public static class GradeSeed
{
    public static List<Grade> GetGrades()
    {
        return new List<Grade>
        {
            new Grade
            {
                SubjectId = 1, EnrollmentId = Guid.Parse("31a19cdd-ed33-4331-9fdc-d5c77d29e0e1"),
                Date = new DateOnly(2021, 01, 01), Percent = 90
            },
            new Grade
            {
                SubjectId = 2, EnrollmentId = Guid.Parse("31a19cdd-ed33-4331-9fdc-d5c77d29e0e1"),
                Date = new DateOnly(2021, 01, 01), Percent = 80
            },
            new Grade
            {
                SubjectId = 3, EnrollmentId = Guid.Parse("31a19cdd-ed33-4331-9fdc-d5c77d29e0e1"),
                Date = new DateOnly(2021, 01, 01), Percent = 70
            }
        };
    }
}