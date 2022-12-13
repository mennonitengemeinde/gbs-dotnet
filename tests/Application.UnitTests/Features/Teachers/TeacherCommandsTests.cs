using Gbs.Application.Features.Teachers;
using Gbs.Shared.Teachers;

namespace Gbs.Tests.Application.UnitTests.Features.Teachers;

public class TeacherCommandsTests : TeacherTestBase
{
    [Fact]
    public async Task Add_AddsTeacher()
    {
        // Arrange
        var cmd = new TeacherCommands(Context, Mapper, Validator);
        var request = new CreateTeacherRequest { Name = "Test Teacher" };
        // Act
        var result = await cmd.Add(request);
        var contextCount = Context.Teachers.Count();
        // Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal(request.Name, result.Data.Name);
        Assert.Equal(4, result.Data.Id);
        Assert.Equal(4, contextCount);
    }

    [Fact]
    public async Task Add_ReturnsValidationError_WhenTeacherNameAlreadyExists()
    {
        // Arrange
        var cmd = new TeacherCommands(Context, Mapper, Validator);
        var request = new CreateTeacherRequest { Name = "Teacher 1" };
        // Act
        var result = await cmd.Add(request);
        var contextCount = Context.Churches.Count();
        // Assert
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal(422, result.StatusCode);
        Assert.Equal(3, contextCount);
    }

    [Fact]
    public async Task Update_UpdatesTeacher()
    {
        // Arrange
        var cmd = new TeacherCommands(Context, Mapper, Validator);
        var request = new CreateTeacherRequest { Name = "Test Teacher" };
        // Act
        var result = await cmd.Update(1, request);
        var contextCount = Context.Teachers.Count();
        // Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal(request.Name, result.Data.Name);
        Assert.Equal(1, result.Data.Id);
        Assert.Equal(3, contextCount);
    }

    [Fact]
    public async Task Update_UpdatesTeacher_WhenNameDoesNotChange()
    {
        // Arrange
        var cmd = new TeacherCommands(Context, Mapper, Validator);
        var request = new CreateTeacherRequest { Name = "Teacher 1" };
        // Act
        var result = await cmd.Update(1, request);
        var contextCount = Context.Teachers.Count();
        // Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal(request.Name, result.Data.Name);
        Assert.Equal(1, result.Data.Id);
        Assert.Equal(3, contextCount);
    }

    [Fact]
    public async Task Update_ReturnsNotFound()
    {
        // Arrange
        var cmd = new TeacherCommands(Context, Mapper, Validator);
        var request = new CreateTeacherRequest { Name = "Teacher 5" };
        // Act
        var result = await cmd.Update(5, request);
        var contextCount = Context.Teachers.Count();
        // Assert
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal(404, result.StatusCode);
        Assert.Equal(3, contextCount);
    }

    [Fact]
    public async Task Update_ReturnsValidationError_WhenNameAlreadyExists()
    {
        // Arrange
        var cmd = new TeacherCommands(Context, Mapper, Validator);
        var request = new CreateTeacherRequest { Name = "Teacher 1" };
        // Act
        var result = await cmd.Update(2, request);
        var contextCount = Context.Teachers.Count();
        // Assert
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal(422, result.StatusCode);
        Assert.Equal(3, contextCount);
    }

    [Fact]
    public async Task Delete_DeletesChurch()
    {
        // Arrange
        var cmd = new TeacherCommands(Context, Mapper, Validator);
        // Act
        var result = await cmd.Delete(1);
        var contextCount = Context.Teachers.Count();
        // Assert
        Assert.True(result.Success);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal(2, contextCount);
    }

    [Fact]
    public async Task Delete_ReturnsNotFound()
    {
        // Arrange
        var cmd = new TeacherCommands(Context, Mapper, Validator);
        // Act
        var result = await cmd.Delete(5);
        var contextCount = Context.Teachers.Count();
        // Assert
        Assert.False(result.Success);
        Assert.Equal(404, result.StatusCode);
        Assert.Equal(3, contextCount);
    }
}