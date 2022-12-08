using Gbs.Application.Features.Generations;

namespace Gbs.Tests.Application.UnitTests.Features.Generations;

public class GenerationQueriesTests : GbsTestBase
{
    [Fact]
    public async Task GetAll_ReturnsAllGenerations()
    {
        var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
        var mapper = config.CreateMapper();
        var generationQ = new GenerationQueries(Context, mapper);

        var result = await generationQ.GetAll();

        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal(3, result.Data.Count);
    }

    [Fact]
    public async Task GetById_ReturnsGeneration()
    {
        var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
        var mapper = config.CreateMapper();
        var generationQ = new GenerationQueries(Context, mapper);

        var result = await generationQ.GetById(1);

        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal(1, result.Data.Id);
    }

    [Fact]
    public async Task GetById_ReturnsNull()
    {
        var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
        var mapper = config.CreateMapper();
        var generationQ = new GenerationQueries(Context, mapper);

        var result = await generationQ.GetById(4);

        Assert.False(result.Success);
        Assert.Null(result.Data);
    }
}