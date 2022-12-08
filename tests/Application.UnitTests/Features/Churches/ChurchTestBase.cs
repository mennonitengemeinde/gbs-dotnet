using Gbs.Application.Features.Churches;
using Gbs.Application.Features.Churches.Validators;

namespace Gbs.Tests.Application.UnitTests.Features.Churches;

public class ChurchTestBase : GbsTestBase
{
    protected CreateChurchValidator CreateChurchValidator { get; }
    protected UpdateChurchValidator UpdateChurchValidator { get; }
    protected IMapper Mapper { get; }

    protected ChurchTestBase()
    {
        CreateChurchValidator = new CreateChurchValidator(Context);
        UpdateChurchValidator = new UpdateChurchValidator(Context);
        var config = new MapperConfiguration(cfg => { cfg.AddProfile(new ChurchMapping()); });
        Mapper = config.CreateMapper();
    }
}