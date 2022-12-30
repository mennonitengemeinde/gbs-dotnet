using Gbs.Application.Features.Generations;
using Gbs.Tests.Application.UnitTests.Common;

namespace Gbs.Tests.Application.UnitTests.Features.Generations;

public class GenerationQueriesTests : GenerationTestBase, IQueryTests
{
    [Fact]
    public async Task GetAll_ReturnsAllRecords()
    {
        var generationQ = new GenerationQueries(Context, Mapper);

        var result = await generationQ.GetAll();

        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal(3, result.Data.Count);
    }

    [Fact]
    public async Task GetAll_ReturnsEmptyList_WhenNoRecords()
    {
        // Arrange
        var q = new GenerationQueries(Context, Mapper);
        Context.Generations.RemoveRange(Context.Generations);
        await Context.SaveChangesAsync();
        // Act
        var result = await q.GetAll();
        // Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Empty(result.Data);
    }

    [Fact]
    public async Task GetById_ReturnsRecord()
    {
        var generationQ = new GenerationQueries(Context, Mapper);

        var result = await generationQ.GetById(1);

        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal(1, result.Data.Id);
    }

    [Fact]
    public async Task GetById_ReturnsNull_WhenNoRecord()
    {
        var generationQ = new GenerationQueries(Context, Mapper);

        var result = await generationQ.GetById(4);

        Assert.False(result.Success);
        Assert.Null(result.Data);
    }
}