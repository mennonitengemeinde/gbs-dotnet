namespace Gbs.Application.Features.Streams.Interfaces;

public interface IStreamQueries
{
    Task<Result<List<StreamResponse>>> GetAllStreams();
    Task<Result<StreamResponse>> GetStreamById(int id, bool liveOnly = false);
}