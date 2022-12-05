using Gbs.Shared.Streams;

namespace Gbs.Application.Streams;

public class StreamQueries : IStreamQueries
{
    private readonly IGbsDbContext _context;
    private readonly IMapper _mapper;

    public StreamQueries(IGbsDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<List<StreamDto>>> GetAllStreams()
    {
        var streams = await _context.Streams
            .ProjectTo<StreamDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return Result.Ok(streams);
    }

    public async Task<Result<StreamDto>> GetStreamById(int id, bool liveOnly = false)
    {
        var liveStream = await _context.Streams
            .Where(s => s.Id == id)
            .ProjectTo<StreamDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return liveStream == null
            ? Result.NotFound<StreamDto>("Stream not found")
            : Result.Ok(liveStream);
    }
}