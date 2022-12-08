using Gbs.Application.Features.Subjects;

namespace Gbs.Tests.Application.UnitTests.Features.Subjects;

public class SubjectQueriesTests : GbsTestBase
{
    [Fact]
    public async Task GetAll_ShouldReturnAllSubjects()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
        var mapper = config.CreateMapper();
        var q = new SubjectQueries(Context, mapper);

        // Act
        var result = await q.GetAll();

        // Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal(3, result.Data.Count);
    }
    
    [Fact]
    public async Task Get_ShouldReturnSubject()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
        var mapper = config.CreateMapper();
        var q = new SubjectQueries(Context, mapper);

        // Act
        var result = await q.GetById(1);

        // Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal("Subject 1", result.Data.Name);
    }
}