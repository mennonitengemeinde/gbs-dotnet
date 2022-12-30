using Gbs.Application.Entities;

namespace Gbs.Tests.Infrastructure.Seeds;

public static class StreamSeed
{
    public static List<LiveStream> GetStreams()
    {
        return new List<LiveStream>
        {
            new LiveStream
            {
                Id = 1,
                Title = "Stream 1",
                Url = "https://www.youtube.com/embed/aqz-KE-bpKQ",
                IsLive = true,
                GenerationId = 1,
            },
            new LiveStream
            {
                Id = 2,
                Title = "Stream 2",
                Url = "https://www.youtube.com/embed/aqz-KE-bpKQ",
                IsLive = false,
                GenerationId = 1,
            },
            new LiveStream
            {
                Id = 3,
                Title = "Stream 3",
                Url = "https://www.youtube.com/embed/aqz-KE-bpKQ",
                IsLive = true,
                GenerationId = 1,
            },
        };
    }
}