using Gbs.Application.Features.Generations;
using Gbs.Application.Features.Generations.Validators;

namespace Gbs.Tests.Application.UnitTests.Features.Generations;

public class GenerationTestBase : GbsTestBase
{
    protected IMapper Mapper { get; }
    protected CreateGenerationValidator CreateGenerationRequestValidator { get; }
    protected UpdateGenerationValidator UpdateGenerationRequestValidator { get; }

    public GenerationTestBase()
    {
        var config = new MapperConfiguration(cfg => { cfg.AddProfile(new GenerationMapping()); });
        Mapper = config.CreateMapper();
        CreateGenerationRequestValidator = new CreateGenerationValidator(Context);
        UpdateGenerationRequestValidator = new UpdateGenerationValidator(Context);
    }
}