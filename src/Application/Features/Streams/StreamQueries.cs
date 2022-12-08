using Gbs.Application.Features.Streams.Interfaces;

namespace Gbs.Application.Features.Streams;

public class StreamQueries : IStreamQueries
{
    private readonly IGbsDbContext _context;
    private readonly IMapper _mapper;

    public StreamQueries(IGbsDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<List<StreamResponse>>> GetAllStreams()
    {
        var streams = await _context.Streams
            .ProjectTo<StreamResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return Result.Ok(streams);
    }

    public async Task<Result<StreamResponse>> GetStreamById(int id, bool liveOnly = false)
    {
        var liveStream = await _context.Streams
            .Where(s => s.Id == id)
            .ProjectTo<StreamResponse>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return liveStream == null
            ? Result.NotFound<StreamResponse>("Stream not found")
            : Result.Ok(liveStream);
    }
}