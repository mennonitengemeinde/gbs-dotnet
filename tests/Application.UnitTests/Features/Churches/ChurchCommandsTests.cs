using Gbs.Application.Features.Churches;
using Gbs.Application.Features.Churches.Validators;
using Gbs.Shared.Churches;

namespace Gbs.Tests.Application.UnitTests.Features.Churches;

public class ChurchCommandsTests : ChurchTestBase
{
    [Fact]
    public async Task Add_AddsChurch()
    {
        // Arrange
        var churchCmd = new ChurchCommands(Context, Mapper, CreateChurchValidator, UpdateChurchValidator);
        var church = new CreateChurchRequest { Name = "Test Church", Country = "Test Country" };
        // Act
        var result = await churchCmd.Add(church);
        var contextCount = Context.Churches.Count();
        // Assert
        Assert.Equal(200, result.StatusCode);
        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal(church.Name, result.Data.Name);
        Assert.Equal(4, result.Data.Id);
        Assert.Equal(4, contextCount);
    }

    [Fact]
    public async Task Add_ReturnsValidationError()
    {
        // Arrange
        var churchCmd = new ChurchCommands(Context, Mapper, CreateChurchValidator, UpdateChurchValidator);
        var church = new CreateChurchRequest { Name = "Church 1", Country = "Test Country" };
        // Act
        var result = await churchCmd.Add(church);
        var contextCount = Context.Churches.Count();
        // Assert
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal(422, result.StatusCode);
        Assert.Equal(3, contextCount);
    }

    [Fact]
    public async Task Update_UpdatesChurch()
    {
        // Arrange
        var churchCmd = new ChurchCommands(Context, Mapper, CreateChurchValidator, UpdateChurchValidator);
        var church = new UpdateChurchRequest { Id = 1, Name = "Test Church", Country = "Test Country" };
        // Act
        var result = await churchCmd.Update(church);
        var contextCount = Context.Churches.Count();
        // Assert
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
        var churchCmd = new ChurchCommands(Context, Mapper, CreateChurchValidator, UpdateChurchValidator);
        var church = new UpdateChurchRequest { Id = 5, Name = "Church 1", Country = "Test Country" };
        // Act
        var result = await churchCmd.Update(church);
        var contextCount = Context.Churches.Count();
        // Assert
        Assert.False(result.Success);
        Assert.Null(result.Data);
        Assert.Equal(404, result.StatusCode);
        Assert.Equal(3, contextCount);
    }

    [Fact]
    public async Task Update_ReturnsValidationError()
    {
        // Arrange
        var churchCmd = new ChurchCommands(Context, Mapper, CreateChurchValidator, UpdateChurchValidator);
        var church = new UpdateChurchRequest { Id = 2, Name = "Church 1", Country = "Test Country" };
        // Act
        var result = await churchCmd.Update(church);
        var contextCount = Context.Churches.Count();
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
        var churchCmd = new ChurchCommands(Context, Mapper, CreateChurchValidator, UpdateChurchValidator);
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
        var churchCmd = new ChurchCommands(Context, Mapper, CreateChurchValidator, UpdateChurchValidator);
        // Act
        var result = await churchCmd.Delete(5);
        var contextCount = Context.Churches.Count();
        // Assert
        Assert.False(result.Success);
        Assert.Equal(404, result.StatusCode);
        Assert.Equal(3, contextCount);
    }
}