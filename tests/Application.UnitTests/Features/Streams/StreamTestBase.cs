using Gbs.Application.Features.Streams;

namespace Gbs.Tests.Application.UnitTests.Features.Streams;

public class StreamTestBase : GbsTestBase
{
    public IMapper Mapper { get; set; }
    
    public StreamTestBase()
    {
        var config = new MapperConfiguration(cfg => { cfg.AddProfile(new StreamMapping()); });
        Mapper = config.CreateMapper();
    }
}