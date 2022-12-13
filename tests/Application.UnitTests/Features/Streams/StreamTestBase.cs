using Gbs.Application.Features.Streams;

namespace Gbs.Tests.Application.UnitTests.Features.Streams;

public class StreamTestBase : GbsTestBase
{
    protected StreamTestBase()
    {
        Validator = new StreamValidator(Context);
    }

    protected StreamValidator Validator { get; set; }
}