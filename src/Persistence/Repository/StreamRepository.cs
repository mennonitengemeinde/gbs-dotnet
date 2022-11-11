namespace Gbs.Infrastructure.Persistence.Repository;

public class StreamRepository : IStreamRepository
{
    private readonly DataContext _context;
    // private readonly IHubContext<LivestreamHub> _streamHub;

    public StreamRepository(DataContext context)
    {
        _context = context;
        // _streamHub = streamHub;
    }

    // private async Task NotifyClients()
    // {
    //     var streams = await GetLiveStreams();
    //     await _streamHub.Clients.All.SendAsync("ReceiveStreams", streams);
    // }

    public async Task<Result<List<StreamDto>>> GetLiveStreams()
    {
        var result = await _context.Streams
            .Select(s => new StreamDto
            {
                Id = s.Id,
                Title = s.Title,
                Url = s.Url,
                GenerationId = s.GenerationId,
                IsLive = s.IsLive,
                GenerationName = s.Generation.Name,
                Teachers = s.StreamTeachers.Select(st => new TeacherDto
                {
                    Id = st.Teacher.Id,
                    Name = st.Teacher.Name,
                    UserId = st.Teacher.UserId
                })
            })
            .ToListAsync();

        return Result.Ok(result);
    }

    public async Task<Result<StreamDto>> GetLiveStreamById(int id, bool onlyLive = false)
    {
        var liveStream = await _context.Streams
            .Where(s => s.Id == id)
            .Select(s => new StreamDto
            {
                Id = s.Id,
                Title = s.Title,
                Url = s.Url,
                GenerationId = s.GenerationId,
                IsLive = s.IsLive,
                GenerationName = s.Generation.Name,
                Teachers = s.StreamTeachers.Select(st => new TeacherDto
                {
                    Id = st.Teacher.Id,
                    Name = st.Teacher.Name,
                    UserId = st.Teacher.UserId
                })
            })
            .FirstOrDefaultAsync();
        return liveStream == null || (onlyLive && !liveStream.IsLive)
            ? Result.BadRequest<StreamDto>("Stream not found")
            : Result.Ok(liveStream);
    }

    public async Task<Result<StreamDto>> CreateLiveStream(StreamCreateDto streamCreateDto)
    {
        if (await _context.Streams.AnyAsync(s => s.Title.ToLower().Equals(streamCreateDto.Title.ToLower())))
            return Result.BadRequest<StreamDto>("Stream with this title already exists");

        var generation = await _context.Generations.FirstOrDefaultAsync(g => g.Id == streamCreateDto.GenerationId);
        if (generation == null)
            return Result.NotFound<StreamDto>("Generation not found");

        var stream = new LiveStream
        {
            Title = streamCreateDto.Title,
            Url = streamCreateDto.Url,
            IsLive = streamCreateDto.IsLive,
            Generation = generation
        };

        stream.StreamTeachers.Clear();

        var streamTeachers = streamCreateDto.Teachers
            .Select(teacherId => new LiveStreamTeacher { TeacherId = teacherId, LiveStreamId = stream.Id })
            .ToList();
        stream.StreamTeachers.AddRange(streamTeachers);
        _context.Streams.Add(stream);
        await _context.SaveChangesAsync();
        // await NotifyClients();
        return await GetLiveStreamById(stream.Id);
    }

    public async Task<Result<StreamDto>> UpdateStream(int streamId, StreamCreateDto liveDto)
    {
        var stream = await _context.Streams
            .Include(s => s.StreamTeachers)
            .FirstOrDefaultAsync(s => s.Id == streamId);

        if (stream == null)
            return Result.NotFound<StreamDto>("Stream not found");

        if (await _context.Streams.AnyAsync(s => s.Title.ToLower().Equals(liveDto.Title.ToLower()) && s.Id != streamId))
            return Result.BadRequest<StreamDto>("Title already exists");

        var generation = await _context.Generations.FirstOrDefaultAsync(g => g.Id == liveDto.GenerationId);
        if (generation == null)
            return Result.NotFound<StreamDto>("Generation not found");

        foreach (var streamTeacher in stream.StreamTeachers
                     .Where(streamTeacher => !liveDto.Teachers.Contains(streamTeacher.TeacherId)))
        {
            _context.StreamTeachers.Remove(streamTeacher);
        }

        foreach (var teacherId in liveDto.Teachers
                     .Where(teacherId => stream.StreamTeachers.All(st => st.TeacherId != teacherId)))
        {
            stream.StreamTeachers.Add(new LiveStreamTeacher { TeacherId = teacherId, LiveStreamId = stream.Id });
        }

        stream.Title = liveDto.Title;
        stream.Url = liveDto.Url;
        stream.IsLive = liveDto.IsLive;
        stream.Generation = generation;
        _context.Streams.Update(stream);
        await _context.SaveChangesAsync();
        // await NotifyClients();
        return await GetLiveStreamById(stream.Id);
    }

    public async Task<Result<StreamDto>> UpdateStreamLiveStatus(int streamId, StreamUpdateLiveDto liveDto)
    {
        var dbStream = await _context.Streams.FirstOrDefaultAsync(s => s.Id == streamId);
        if (dbStream == null)
        {
            return Result.NotFound<StreamDto>("Stream not found");
        }

        dbStream.IsLive = liveDto.IsLive;
        await _context.SaveChangesAsync();
        // await NotifyClients();
        return await GetLiveStreamById(dbStream.Id);
    }

    public async Task<Result<bool>> DeleteStream(int streamId)
    {
        var stream = await _context.Streams.FirstOrDefaultAsync(s => s.Id == streamId);
        if (stream == null)
            return Result.BadRequest<bool>("Stream not found");
        _context.Streams.Remove(stream);
        await _context.SaveChangesAsync();
        // await NotifyClients();
        return Result.Ok(true, "Stream deleted successfully");
    }
}