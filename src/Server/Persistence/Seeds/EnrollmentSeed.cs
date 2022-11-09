namespace Gbs.Server.Persistence.Seeds;

public static class EnrollmentSeed
{
    public static List<Enrollment> GetEnrollments()
    {
        return new List<Enrollment>
        {
            new Enrollment
            {
                Id = Guid.Parse("31a19cdd-ed33-4331-9fdc-d5c77d29e0e1"),
                StudentId = 1,
                GenerationId = 1,
                HasCompleted = true,
                EnrollmentDate = new DateOnly(2021, 1, 1),
                CompletionDate = new DateOnly(2021, 1, 2),
                Testimony = "Testimony 1",
                AgreedToGbsConcept = true
            },
            new Enrollment
            {
                Id = Guid.Parse("31a19cdd-ed33-4331-9fdc-d5c77d29e0e2"),
                StudentId = 2,
                GenerationId = 1,
                IsActive = true,
                EnrollmentDate = new DateOnly(2021, 1, 1),
                Testimony = "Testimony 2",
                AgreedToGbsConcept = true
            },
            new Enrollment
            {
                Id = Guid.Parse("31a19cdd-ed33-4331-9fdc-d5c77d29e0e3"),
                StudentId = 3,
                GenerationId = 1,
                IsActive = true,
                EnrollmentDate = new DateOnly(2021, 1, 1),
                Testimony = "Testimony 3",
                AgreedToGbsConcept = true
            },
        };
    }
}