using Gbs.Application.Features.Streams.Interfaces;

namespace Gbs.Application.Features.Streams;

public class StreamQueries(IGbsDbContext context, IMapper mapper) : IStreamQueries
{
    public async Task<Result<List<StreamResponse>>> GetAllStreams()
    {
        var streams = await context.Streams
            .ProjectTo<StreamResponse>(mapper.ConfigurationProvider)
            .ToListAsync();

        return Result.Ok(streams);
    }

    public async Task<Result<StreamResponse>> GetStreamById(int id, bool liveOnly = false)
    {
        var liveStream = await context.Streams
            .Where(s => s.Id == id)
            .ProjectTo<StreamResponse>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return liveStream == null
            ? Result.NotFound<StreamResponse>("Stream not found")
            : Result.Ok(liveStream);
    }
}