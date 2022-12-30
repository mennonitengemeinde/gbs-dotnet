using Gbs.Application.Features.Teachers;

namespace Gbs.Tests.Application.UnitTests.Features.Teachers;

public class TeacherQueriesTests : TeacherTestBase
{
    [Fact]
    public async Task GetAll_ReturnsAllTeachers()
    {
        // Arrange
        var q = new TeacherQueries(Context, Mapper);
        // Act
        var result = await q.GetAll();
        // Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal(3, result.Data.Count);
    }

    [Fact]
    public async Task GetById_ReturnsTeacher()
    {
        // Arrange
        var q = new TeacherQueries(Context, Mapper);
        // Act
        var result = await q.GetById(1);
        // Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal(1, result.Data.Id);
    }
}