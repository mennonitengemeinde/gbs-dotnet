using Gbs.Application.Features.Churches;

namespace Gbs.Tests.Application.UnitTests.Features.Churches;

public class ChurchTestBase : GbsTestBase
{
    protected ChurchValidator ChurchValidator { get; }

    protected ChurchTestBase()
    {
        ChurchValidator = new ChurchValidator(Context);
    }
}