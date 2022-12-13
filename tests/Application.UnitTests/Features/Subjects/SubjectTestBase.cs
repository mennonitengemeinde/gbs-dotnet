using Gbs.Application.Features.Subjects;

namespace Gbs.Tests.Application.UnitTests.Features.Subjects;

public class SubjectTestBase : GbsTestBase
{
    protected SubjectTestBase()
    {
        Validator = new SubjectValidator(Context);
    }

    protected SubjectValidator Validator { get; set; }
}