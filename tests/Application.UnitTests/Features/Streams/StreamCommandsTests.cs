using Gbs.Application.Features.Streams;
using Gbs.Shared.Streams;

namespace Gbs.Tests.Application.UnitTests.Features.Streams;

public class StreamCommandsTests : StreamTestBase
{
    [Fact]
    public async Task CreateStream_ShouldCreateStream()
    {
        var cmd = new StreamCommands(Context, Mapper, Validator);
        var stream = new CreateStreamRequest
        {
            Title = "Stream 4",
            Url = "https://www.youtube.com/embed/aqz-KE-bpKQ",
            IsLive = true,
            GenerationId = 1,
            Teachers = new List<int> { 1 }
        };

        var result = await cmd.CreateStream(stream);

        Assert.True(result.Success);
        Assert.Equal(4, result.Data?.Id);
    }
}