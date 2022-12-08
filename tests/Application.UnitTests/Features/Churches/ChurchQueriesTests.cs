using Gbs.Application.Features.Churches;

namespace Gbs.Tests.Application.UnitTests.Features.Churches;

public class ChurchQueriesTests : GbsTestBase
{
    IMapper Mapper { get; }
    
    public ChurchQueriesTests()
    {
        var config = new MapperConfiguration(cfg => { cfg.AddProfile(new ChurchMapping()); });
        Mapper = config.CreateMapper();
    }
    
    [Fact]
    public async Task GetAll_ReturnsAllChurches()
    {
        
        var churchQ = new ChurchQueries(Context, Mapper);
        
        var result = await churchQ.GetAll();

        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal(3, result.Data.Count);
    }
    
    [Fact]
    public async Task GetById_ReturnsChurch()
    {
        var churchQ = new ChurchQueries(Context, Mapper);
        
        var result = await churchQ.GetById(1);

        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal(1, result.Data.Id);
    }
}