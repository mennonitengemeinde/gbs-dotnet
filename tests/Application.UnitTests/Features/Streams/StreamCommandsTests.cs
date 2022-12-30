using Gbs.Application.Features.Streams;
using Gbs.Shared.Streams;

namespace Gbs.Tests.Application.UnitTests.Features.Streams;

public class StreamCommandsTests : StreamTestBase, ICommandTests
{
    [Fact]
    public async Task Add_AddsNewEntity()
    {
        // Arrange
        var cmd = new StreamCommands(Context, Mapper, Validator);
        var stream = new CreateStreamRequest
        {
            Title = "Stream 4",
            Url = "https://www.youtube.com/embed/aqz-KE-bpKQ",
            IsLive = true,
            GenerationId = 1,
            Teachers = new List<int> { 1 }
        };
        // Act
        var result = await cmd.CreateStream(stream);
        // Assert
        Assert.True(result.Success);
        Assert.Equal(4, result.Data?.Id);
    }

    [Fact]
    public async Task Add_ReturnValidationError_WhenEntityAlreadyExists()
    {
        // Arrange
        var cmd = new StreamCommands(Context, Mapper, Validator);
        var stream = new CreateStreamRequest
        {
            Title = "Stream 1",
            Url = "https://www.youtube.com/embed/aqz-KE-bpKQ",
            IsLive = true,
            GenerationId = 1,
            Teachers = new List<int> { 1 }
        };
        // Act
        var result = await cmd.CreateStream(stream);
        // Assert
        Assert.False(result.Success);
        Assert.Equal(422, result.StatusCode);
    }

    [Fact]
    public async Task Update_UpdatesEntity()
    {
        // Arrange
        var cmd = new StreamCommands(Context, Mapper, Validator);
        var stream = new CreateStreamRequest
        {
            Title = "Stream 5",
            Url = "https://www.youtube.com/embed/aqz-KE-bpKQ",
            IsLive = true,
            GenerationId = 1,
            Teachers = new List<int> { 1, 2, 3 }
        };
        // Act
        var result = await cmd.UpdateStream(1, stream);
        // Assert
        Assert.True(result.Success);
        Assert.Equal(1, result.Data?.Id);
        Assert.Equal("Stream 5", result.Data?.Title);
        Assert.Equal(3, Context.Streams.FirstOrDefault(x => x.Id == 1)!.StreamTeachers.Count());
    }

    [Fact]
    public async Task Update_ReturnsNotFound_WhenEntityDoesNotExist()
    {
        // Arrange
        var cmd = new StreamCommands(Context, Mapper, Validator);
        var stream = new CreateStreamRequest
        {
            Title = "Stream 5",
            Url = "https://www.youtube.com/embed/aqz-KE-bpKQ",
            IsLive = true,
            GenerationId = 1,
            Teachers = new List<int> { 1 }
        };
        // Act
        var result = await cmd.UpdateStream(5, stream);
        // Assert
        Assert.False(result.Success);
        Assert.Equal(404, result.StatusCode);
    }
    
    [Fact]
    public async Task Update_ReturnsNotFound_WhenGenerationDoesNotExist()
    {
        // Arrange
        var cmd = new StreamCommands(Context, Mapper, Validator);
        var stream = new CreateStreamRequest
        {
            Title = "Stream 5",
            Url = "https://www.youtube.com/embed/aqz-KE-bpKQ",
            IsLive = true,
            GenerationId = 5,
            Teachers = new List<int> { 1 }
        };
        // Act
        var result = await cmd.UpdateStream(1, stream);
        // Assert
        Assert.False(result.Success);
        Assert.Equal(404, result.StatusCode);
    }

    [Fact]
    public async Task Update_ReturnValidationError_WhenEntityAlreadyExists()
    {
        // Arrange
        var cmd = new StreamCommands(Context, Mapper, Validator);
        var stream = new CreateStreamRequest
        {
            Title = "Stream 2",
            Url = "https://www.youtube.com/embed/aqz-KE-bpKQ",
            IsLive = true,
            GenerationId = 1,
            Teachers = new List<int> { 1 }
        };
        // Act
        var result = await cmd.UpdateStream(1, stream);
        // Assert
        Assert.False(result.Success);
        Assert.Equal(422, result.StatusCode);
    }

    [Fact]
    public async Task Delete_DeletesEntity()
    {
        // Arrange
        var cmd = new StreamCommands(Context, Mapper, Validator);
        // Act
        var result = await cmd.DeleteStream(1);
        // Assert
        Assert.True(result.Success);
        Assert.True(result.Data);
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_WhenEntityDoesNotExist()
    {
        // Arrange
        var cmd = new StreamCommands(Context, Mapper, Validator);
        // Act
        var result = await cmd.DeleteStream(5);
        // Assert
        Assert.False(result.Success);
        Assert.Equal(404, result.StatusCode);
    }

    [Fact]
    public async Task UpdateLiveStatus_UpdatesEntity()
    {
        // Arrange
        var cmd = new StreamCommands(Context, Mapper, Validator);
        var request = new UpdateStreamLiveRequest { IsLive = false };
        // Act
        var result = await cmd.UpdateLiveStatus(1, request);
        // Assert
        Assert.True(result.Success);
        Assert.False(result.Data?.IsLive);
    }
    
    [Fact]
    public async Task UpdateLiveStatus_ReturnsNotFound_WhenEntityDoesNotExist()
    {
        // Arrange
        var cmd = new StreamCommands(Context, Mapper, Validator);
        var request = new UpdateStreamLiveRequest { IsLive = false };
        // Act
        var result = await cmd.UpdateLiveStatus(5, request);
        // Assert
        Assert.False(result.Success);
        Assert.Equal(404, result.StatusCode);
    }
}