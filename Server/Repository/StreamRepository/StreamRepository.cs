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
        response.Data = await _context.Streams.ToListAsync();
        return response;
    }

    public async Task<ServiceResponse<LiveStream>> CreateLiveStream(CreateStreamDto createStreamDto)
    {
        var response = new ServiceResponse<LiveStream>();
        if (await _context.Streams.AnyAsync(s => s.Title == createStreamDto.Title))
        {
            response.Success = false;
            response.Message = "Stream already exists";
            return response;
        }
        var stream = new LiveStream
        {
            Title = createStreamDto.Title,
            Url = createStreamDto.Url,
            IsLive = createStreamDto.IsLive,
            GenerationId = createStreamDto.GenerationId
        };
        _context.Streams.Add(stream);
        stream.Teachers.AddRange(createStreamDto.Teachers);
        await _context.SaveChangesAsync();
        response.Data = stream;
        return response;
    }
}