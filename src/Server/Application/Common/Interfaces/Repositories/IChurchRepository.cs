namespace Gbs.Server.Application.Common.Interfaces.Repositories;

public interface IChurchRepository
{
    Task<Result<List<ChurchDto>>> GetAllChurches();
    Task<Result<ChurchDto>> GetChurchById(int id);
    Task<Result<ChurchDto>> AddChurch(ChurchCreateDto church);
    Task<Result<ChurchDto>> UpdateChurch(int id, ChurchCreateDto churchDto);
    Task<Result<bool>> DeleteChurch(int id);
}