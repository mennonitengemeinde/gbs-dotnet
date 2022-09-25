namespace gbs.Server.Repository.ChurchRepository;

public interface IChurchRepository
{
    Task<ServiceResponse<List<ChurchDto>>> GetAllChurches();
    Task<ServiceResponse<ChurchDto>> GetChurchById(int id);
    Task<ServiceResponse<List<ChurchDto>>> AddChurch(ChurchCreateDto church);
    Task<ServiceResponse<List<ChurchDto>>> UpdateChurch(int id, ChurchCreateDto churchDto);
    Task<ServiceResponse<List<ChurchDto>>> DeleteChurch(int id);
}