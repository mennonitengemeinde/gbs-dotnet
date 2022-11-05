namespace Gbs.Server.Application.Common.Interfaces.Repositories;

public interface IChurchRepository
{
    Task<Result<List<ChurchDto>>> GetAllChurches();
    Task<Result<ChurchDto>> GetChurchById(int id);
    Task<Result<List<ChurchDto>>> AddChurch(ChurchCreateDto church);
    Task<Result<List<ChurchDto>>> UpdateChurch(int id, ChurchCreateDto churchDto);
    Task<Result<List<ChurchDto>>> DeleteChurch(int id);
}