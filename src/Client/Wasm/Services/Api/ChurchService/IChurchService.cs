namespace gbs.Client.Services.Api.ChurchService;

public interface IChurchService
{
    List<ChurchDto> Churches { get; set; }
    event Action ChurchesChanged;
    Task<ServiceResponse<List<ChurchDto>>> GetChurches();
    Task<ServiceResponse<ChurchDto>> GetChurch(int id);
    Task<ServiceResponse<List<ChurchDto>>> AddChurch(ChurchCreateDto church);
    Task<ServiceResponse<List<ChurchDto>>> UpdateChurch(int churchId, ChurchCreateDto church);
    Task<ServiceResponse<List<ChurchDto>>> DeleteChurch(int id);
}