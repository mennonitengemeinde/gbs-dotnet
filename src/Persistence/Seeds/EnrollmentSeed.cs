namespace Gbs.Infrastructure.Persistence.Seeds;

public static class EnrollmentSeed
{
    public static List<Enrollment> GetEnrollments()
    {
        return new List<Enrollment>
        {
            new Enrollment
            {
                Id = 1,
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
                Id = 2,
                StudentId = 2,
                GenerationId = 1,
                IsActive = true,
                EnrollmentDate = new DateOnly(2021, 1, 1),
                Testimony = "Testimony 2",
                AgreedToGbsConcept = true
            },
            new Enrollment
            {
                Id = 3,
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