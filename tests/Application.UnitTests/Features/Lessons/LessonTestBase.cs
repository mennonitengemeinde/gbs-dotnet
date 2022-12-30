using Gbs.Application.Features.Lessons;

namespace Gbs.Tests.Application.UnitTests.Features.Lessons;

public class LessonTestBase : GbsTestBase
{
    protected LessonTestBase()
    {
        Validator = new LessonValidator(Context);
    }

    protected LessonValidator Validator { get; }
}