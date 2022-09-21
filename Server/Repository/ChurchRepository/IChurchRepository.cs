namespace gbs.Server.Repository.ChurchRepository;

public interface IChurchRepository
{
    Task<ServiceResponse<List<Church>>> GetAllChurches();
    Task<ServiceResponse<Church>> GetChurchById(int id);
    Task<ServiceResponse<List<Church>>> AddChurch(ChurchCreateDto church);
    Task<ServiceResponse<List<Church>>> UpdateChurch(int id, ChurchCreateDto churchDto);
    Task<ServiceResponse<List<Church>>> DeleteChurch(int id);
}