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

    public async Task<ServiceResponse<LiveStream>> CreateLiveStream(StreamCreateDto streamCreateDto)
    {
        var response = new ServiceResponse<LiveStream>();
        if (await _context.Streams.AnyAsync(s => s.Title == streamCreateDto.Title))
        {
            response.Success = false;
            response.Message = "Stream already exists";
            return response;
        }
        var stream = new LiveStream
        {
            Title = streamCreateDto.Title,
            Url = streamCreateDto.Url,
            IsLive = streamCreateDto.IsLive,
            GenerationId = streamCreateDto.GenerationId
        };
        _context.Streams.Add(stream);
        await _context.SaveChangesAsync();
        response.Data = stream;
        return response;
    }
}