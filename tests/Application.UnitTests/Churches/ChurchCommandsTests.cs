using Gbs.Shared.Churches;

namespace Gbs.Tests.Application.UnitTests.Churches;

public class ChurchCommandsTests : GbsTestBase
{
    [Fact]
    public async Task Add_AddsChurch()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
        var mapper = config.CreateMapper();
        var churchCmd = new ChurchCommands(Context, mapper);
        var church = new CreateChurchRequest { Name = "Test Church", Country = "Test Country" };
        // Act
        var result = await churchCmd.Add(church);
        var contextCount = Context.Churches.Count();
        // Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal(church.Name, result.Data.Name);
        Assert.Equal(4, result.Data.Id);
        Assert.Equal(4, contextCount);
    }

    [Fact]
    public async Task Add_ReturnsBadRequest()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
        var mapper = config.CreateMapper();
        var churchCmd = new ChurchCommands(Context, mapper);
        var church = new CreateChurchRequest { Name = "Church 1", Country = "Test Country" };
        // Act
        var result = await churchCmd.Add(church);
        var contextCount = Context.Churches.Count();
        // Assert
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal(400, result.StatusCode);
        Assert.Equal(3, contextCount);
    }

    [Fact]
    public async Task Update_UpdatesChurch()
    {
        var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
        var mapper = config.CreateMapper();

        var churchCmd = new ChurchCommands(Context, mapper);

        var church = new CreateChurchRequest { Name = "Test Church", Country = "Test Country" };

        var result = await churchCmd.Update(1, church);
        var contextCount = Context.Churches.Count();

        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal(church.Name, result.Data.Name);
        Assert.Equal(1, result.Data.Id);
        Assert.Equal(3, contextCount);
    }
    
    [Fact]
    public async Task Update_ReturnsNotFound()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
        var mapper = config.CreateMapper();
        var churchCmd = new ChurchCommands(Context, mapper);
        var church = new CreateChurchRequest { Name = "Church 1", Country = "Test Country" };
        // Act
        var result = await churchCmd.Update(5, church);
        var contextCount = Context.Churches.Count();
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
        var churchCmd = new ChurchCommands(Context, mapper);
        var church = new CreateChurchRequest { Name = "Church 1", Country = "Test Country" };
        // Act
        var result = await churchCmd.Update(2, church);
        var contextCount = Context.Churches.Count();
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
        var churchCmd = new ChurchCommands(Context, mapper);
        // Act
        var result = await churchCmd.Delete(1);
        var contextCount = Context.Churches.Count();
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
        var churchCmd = new ChurchCommands(Context, mapper);
        // Act
        var result = await churchCmd.Delete(5);
        var contextCount = Context.Churches.Count();
        // Assert
        Assert.False(result.Success);
        Assert.Equal(404, result.StatusCode);
        Assert.Equal(3, contextCount);
    }
}