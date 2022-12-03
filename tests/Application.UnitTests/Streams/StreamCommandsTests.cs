using Gbs.Application.Streams;
using Gbs.Domain.Entities;
using Gbs.Shared.Streams;
using Gbs.Tests.Infrastructure.Seeds;

namespace Gbs.Tests.Application.UnitTests.Streams;

public class StreamCommandsTests
{
    private readonly StreamCommands _streamCommands;

    public StreamCommandsTests()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "GbsTest")
            .Options;

        var context = new DataContext(options);
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        var streamList = StreamSeed.GetStreams();
        var teacherList = TeacherSeed.GetTeachers();
        var generationList = GenerationSeed.GetGenerations();
        context.Generations.AddRange(generationList);
        context.Teachers.AddRange(teacherList);
        context.Streams.AddRange(streamList);
        context.SaveChanges();
        context.Streams.FirstOrDefault(s => s.Id == 1)!.StreamTeachers
            .Add(new LiveStreamTeacher { LiveStreamId = 1, TeacherId = 1 });
        context.SaveChanges();
        context.Streams.FirstOrDefault(s => s.Id == 2)!.StreamTeachers
            .Add(new LiveStreamTeacher { LiveStreamId = 2, TeacherId = 1 });
        context.SaveChanges();
        context.Streams.FirstOrDefault(s => s.Id == 3)!.StreamTeachers
            .Add(new LiveStreamTeacher { LiveStreamId = 3, TeacherId = 1 });
        context.SaveChanges();

        var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
        var mapper = config.CreateMapper();

        _streamCommands = new StreamCommands(context, mapper);
    }

    [Fact]
    public async Task CreateStream_ShouldCreateStream()
    {
        var stream = new StreamCreateDto
        {
            Title = "Stream 4",
            Url = "https://www.youtube.com/embed/aqz-KE-bpKQ",
            IsLive = true,
            GenerationId = 1,
            Teachers = new List<int> { 1 }
        };

        var result = await _streamCommands.CreateStream(stream);

        Assert.True(result.Success);
        Assert.Equal(4, result.Data?.Id);
    }
}