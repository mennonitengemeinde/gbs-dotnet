using Gbs.Application.Churches;

namespace Gbs.Tests.Application.UnitTests.Churches;

public class ChurchQueriesTests : GbsTestBase
{
    [Fact]
    public async Task GetAll_ReturnsAllChurches()
    {
        var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
        var mapper = config.CreateMapper();
        var churchQ = new ChurchQueries(Context, mapper);
        
        var result = await churchQ.GetAll();

        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal(3, result.Data.Count);
    }
    
    [Fact]
    public async Task GetById_ReturnsChurch()
    {
        var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
        var mapper = config.CreateMapper();
        var churchQ = new ChurchQueries(Context, mapper);
        
        var result = await churchQ.GetById(1);

        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal(1, result.Data.Id);
    }
}