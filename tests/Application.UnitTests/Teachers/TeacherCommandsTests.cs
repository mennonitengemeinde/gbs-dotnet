using Gbs.Application.Teachers;

namespace Gbs.Tests.Application.UnitTests.Teachers;

public class TeacherCommandsTests : GbsTestBase
{
    [Fact]
    public async Task Add_AddsTeacher()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
        var mapper = config.CreateMapper();
        var cmd = new TeacherCommands(Context, mapper);
        var request = new TeacherCreateDto { Name = "Test Teacher" };
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
    public async Task Add_ReturnsBadRequest()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
        var mapper = config.CreateMapper();
        var cmd = new TeacherCommands(Context, mapper);
        var request = new TeacherCreateDto { Name = "Teacher 1" };
        // Act
        var result = await cmd.Add(request);
        var contextCount = Context.Churches.Count();
        // Assert
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal(400, result.StatusCode);
        Assert.Equal(3, contextCount);
    }

    [Fact]
    public async Task Update_UpdatesTeacher()
    {
        var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
        var mapper = config.CreateMapper();

        var cmd = new TeacherCommands(Context, mapper);

        var request = new TeacherCreateDto { Name = "Test Teacher" };

        var result = await cmd.Update(1, request);
        var contextCount = Context.Teachers.Count();

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
        var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
        var mapper = config.CreateMapper();
        var cmd = new TeacherCommands(Context, mapper);
        var request = new TeacherCreateDto { Name = "Teacher 5" };
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
    public async Task Update_ReturnsBadRequest()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
        var mapper = config.CreateMapper();
        var cmd = new TeacherCommands(Context, mapper);
        var request = new TeacherCreateDto { Name = "Teacher 1" };
        // Act
        var result = await cmd.Update(2, request);
        var contextCount = Context.Teachers.Count();
        // Assert
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal(400, result.StatusCode);
        Assert.Equal(3, contextCount);
    }
    
    [Fact]
    public async Task Delete_DeletesChurch()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
        var mapper = config.CreateMapper();
        var cmd = new TeacherCommands(Context, mapper);
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
        var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
        var mapper = config.CreateMapper();
        var cmd = new TeacherCommands(Context, mapper);
        // Act
        var result = await cmd.Delete(5);
        var contextCount = Context.Teachers.Count();
        // Assert
        Assert.False(result.Success);
        Assert.Equal(404, result.StatusCode);
        Assert.Equal(3, contextCount);
    }
}