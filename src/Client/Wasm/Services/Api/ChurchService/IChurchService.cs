namespace gbs.Client.Wasm.Services.Api.ChurchService;

public interface IChurchService
{
    List<ChurchDto> Churches { get; set; }
    event Action ChurchesChanged;
    Task<Result<List<ChurchDto>>> GetChurches();
    Task<Result<ChurchDto>> GetChurch(int id);
    Task<Result<ChurchDto>> AddChurch(ChurchCreateDto church);
    Task<Result<ChurchDto>> UpdateChurch(int churchId, ChurchCreateDto church);
    Task<Result<bool>> DeleteChurch(int id);
}