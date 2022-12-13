using Gbs.Application.Features.Churches;
using Gbs.Tests.Application.UnitTests.Common;

namespace Gbs.Tests.Application.UnitTests.Features.Churches;

public class ChurchQueriesTests : ChurchTestBase, IQueryTests
{
    [Fact]
    public async Task GetAll_ReturnsAllRecords()
    {
        var churchQ = new ChurchQueries(Context, Mapper);

        var result = await churchQ.GetAll();

        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal(3, result.Data.Count);
    }

    [Fact]
    public async Task GetAll_ReturnsEmptyList_WhenNoRecords()
    {
        // Arrange
        var churchQ = new ChurchQueries(Context, Mapper);
        Context.Churches.RemoveRange(Context.Churches);
        await Context.SaveChangesAsync();
        // Act
        var result = await churchQ.GetAll();
        // Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Empty(result.Data);
    }

    [Fact]
    public async Task GetById_ReturnsRecord()
    {
        var churchQ = new ChurchQueries(Context, Mapper);

        var result = await churchQ.GetById(1);

        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal(1, result.Data.Id);
    }

    public Task GetById_ReturnsNull_WhenNoRecord()
    {
        throw new NotImplementedException();
    }
}