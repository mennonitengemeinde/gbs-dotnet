using Gbs.Application.Features.Generations;

namespace Gbs.Tests.Application.UnitTests.Features.Generations;

public class GenerationTestBase : GbsTestBase
{
    protected GenerationValidator Validator { get; set; }

    protected GenerationTestBase()
    {
        Validator = new GenerationValidator(Context);
    }
}