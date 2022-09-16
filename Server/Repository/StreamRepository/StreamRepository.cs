namespace gbs.Server.Repository.StreamRepository;

public class StreamRepository : IStreamRepository
{
    private readonly DataContext _context;

    public StreamRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<ServiceResponse<List<LiveStream>>> GetLiveStreams()
    {
        ServiceResponse<List<LiveStream>> response = new ServiceResponse<List<LiveStream>>();
        response.Data = await _context.Streams
            .Include(s => s.Generation)
            .Include(s => s.Teachers)
            .ToListAsync();
        return response;
    }

    public async Task<ServiceResponse<LiveStream>> CreateLiveStream(StreamCreateDto streamCreateDto)
    {
        var response = new ServiceResponse<LiveStream>();
        if (await _context.Streams.AnyAsync(s => s.Title == streamCreateDto.Title))
        {
            response.Success = false;
            response.Message = "Stream already exists";
            return response;
        }

        var generation = await _context.Generations.FirstOrDefaultAsync(g => g.Id == streamCreateDto.GenerationId);
        if (generation == null)
        {
            response.Success = false;
            response.Message = "Generation not found";
            return response;
        }

        var stream = new LiveStream
        {
            Title = streamCreateDto.Title,
            Url = streamCreateDto.Url,
            IsLive = streamCreateDto.IsLive,
            Generation = generation
        };
        var teachers = await _context.Teachers
            .Where(t => streamCreateDto.Teachers.Contains(t.Id))
            .ToListAsync();
        stream.Teachers.AddRange(teachers);
        _context.Streams.Add(stream);
        await _context.SaveChangesAsync();
        response.Data = stream;
        return response;
    }

    public async Task<ServiceResponse<LiveStream>> UpdateStreamLiveStatus(int streamId, StreamUpdateLiveDto liveDto)
    {
        var dbStream = await _context.Streams.FirstOrDefaultAsync(s => s.Id == streamId);
        if (dbStream == null)
        {
            return new ServiceResponse<LiveStream>
            {
                Success = false,
                Message = "Stream not found"
            };
        }

        dbStream.IsLive = liveDto.IsLive;
        await _context.SaveChangesAsync();
        return new ServiceResponse<LiveStream> { Data = dbStream };
    }
}