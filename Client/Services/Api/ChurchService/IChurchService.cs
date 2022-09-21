namespace gbs.Client.Services.Api.ChurchService;

public interface IChurchService
{
    List<Church> Churches { get; set; }
    event Action ChurchesChanged;
    Task<ServiceResponse<List<Church>>> GetChurches();
    Task<ServiceResponse<Church>> GetChurch(int id);
    Task<ServiceResponse<List<Church>>> AddChurch(ChurchCreateDto church);
    Task<ServiceResponse<List<Church>>> UpdateChurch(int churchId, ChurchCreateDto church);
    Task<ServiceResponse<List<Church>>> DeleteChurch(int id);
}