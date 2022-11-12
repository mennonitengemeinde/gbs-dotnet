namespace Gbs.Application.Streams;

public class StreamQueries : IStreamQueries
{
    private readonly IStreamRepository _streamRepo;

    public StreamQueries(IStreamRepository streamRepo)
    {
        _streamRepo = streamRepo;
    }

    public async Task<Result<List<StreamDto>>> GetAllStreams()
    {
        var streams = await _streamRepo.GetLiveStreams();
        return Result.Ok(streams);
    }

    public async Task<Result<StreamDto>> GetStreamById(int id, bool liveOnly = false)
    {
        var stream = await _streamRepo.GetLiveStreamById(id);
        return stream == null 
            ? Result.NotFound<StreamDto>("Stream not found") 
            : Result.Ok(stream);
    }
}