using Gbs.Application.Features.Generations;
using Gbs.Application.Features.Generations.Validators;

namespace Gbs.Tests.Application.UnitTests.Features.Generations;

public class GenerationTestBase : GbsTestBase
{
    protected CreateGenerationValidator CreateGenerationRequestValidator { get; }
    protected UpdateGenerationValidator UpdateGenerationRequestValidator { get; }

    protected GenerationTestBase()
    {
        CreateGenerationRequestValidator = new CreateGenerationValidator(Context);
        UpdateGenerationRequestValidator = new UpdateGenerationValidator(Context);
    }
}