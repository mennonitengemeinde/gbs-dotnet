namespace Gbs.Infrastructure.Persistence.Repository;

public class StreamRepository : IStreamRepository
{
    private readonly DataContext _context;

    public StreamRepository(DataContext context)
    {
        _context = context;
    }
    
    public async Task<List<StreamDto>> GetLiveStreams()
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

        return result;
    }

    public async Task<StreamDto?> GetLiveStreamById(int id, bool onlyLive = false)
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

        return liveStream;
    }

    public async Task<Result<int>> CreateLiveStream(StreamCreateDto streamCreateDto)
    {
        var generation = await _context.Generations.FirstOrDefaultAsync(g => g.Id == streamCreateDto.GenerationId);
        if (generation == null)
            return Result.NotFound<int>("Generation not found");
        
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
        return Result.Ok(stream.Id);
    }

    public async Task<Result<bool>> UpdateStream(int streamId, StreamCreateDto liveDto)
    {
        var stream = await _context.Streams
            .Include(s => s.StreamTeachers)
            .FirstOrDefaultAsync(s => s.Id == streamId);

        if (stream == null)
            return Result.NotFound<bool>("Stream not found");

        var generation = await _context.Generations.FirstOrDefaultAsync(g => g.Id == liveDto.GenerationId);
        if (generation == null)
            return Result.NotFound<bool>("Generation not found");

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
        return Result.Ok(true);
    }

    public async Task<Result<bool>> UpdateStreamLiveStatus(int streamId, StreamUpdateLiveDto liveDto)
    {
        var dbStream = await _context.Streams.FirstOrDefaultAsync(s => s.Id == streamId);
        if (dbStream == null)
            return Result.NotFound<bool>("Stream not found");

        dbStream.IsLive = liveDto.IsLive;
        await _context.SaveChangesAsync();
        return Result.Ok(true);
    }

    public async Task<Result<bool>> DeleteStream(int streamId)
    {
        var stream = await _context.Streams.FirstOrDefaultAsync(s => s.Id == streamId);
        if (stream == null)
            return Result.BadRequest<bool>("Stream not found");
        _context.Streams.Remove(stream);
        await _context.SaveChangesAsync();
        return Result.Ok(true, "Stream deleted successfully");
    }

    public async Task<bool> StreamWithTitleExists(string title)
    {
        return await _context.Streams.AnyAsync(s => s.Title.ToLower().Equals(title.ToLower()));
    }
}