namespace Gbs.Application.Common.Interfaces.Queries;

public interface IStreamQueries
{
    Task<Result<List<StreamDto>>> GetAllStreams();
    Task<Result<StreamDto>> GetStreamById(int id, bool liveOnly = false);
}