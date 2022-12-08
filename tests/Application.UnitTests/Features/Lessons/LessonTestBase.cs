using Gbs.Application.Features.Lessons.Validators;

namespace Gbs.Tests.Application.UnitTests.Features.Lessons;

public class LessonTestBase : GbsTestBase
{
    protected CreateLessonValidator CreateLessonValidator { get; }
    protected UpdateLessonValidator UpdateLessonValidator { get; }

    protected LessonTestBase()
    {
        CreateLessonValidator = new CreateLessonValidator(Context);
        UpdateLessonValidator = new UpdateLessonValidator(Context);
    }
}