namespace Gbs.Wasm.Services.Api.ChurchService;

public class ChurchService : IChurchService
{
    private readonly HttpClient _http;
    private readonly IUiService _uiService;
    public List<ChurchDto> Churches { get; set; } = new();
    public event Action? ChurchesChanged;

    public ChurchService(HttpClient http, IUiService uiService)
    {
        _http = http;
        _uiService = uiService;
    }

    private async Task UpdateChurches(Result<List<ChurchDto>> response)
    {
        if (!response.Success)
        {
            await _uiService.ShowErrorAlert(response.Message, response.StatusCode);
            Churches = new List<ChurchDto>();
            return;
        }

        Churches = response.Data ?? new List<ChurchDto>();
        ChurchesChanged?.Invoke();
    }

    public async Task<Result<List<ChurchDto>>> GetChurches()
    {
        var result = await _http.GetAsync("api/churches")
            .EnsureSuccess<List<ChurchDto>>();
        await UpdateChurches(result);
        return result;
    }

    public async Task<Result<ChurchDto>> GetChurch(int id)
    {
        return await _http.GetAsync($"api/churches/{id}")
            .EnsureSuccess<ChurchDto>();
    }

    public async Task<Result<ChurchDto>> AddChurch(ChurchCreateDto church)
    {
        var result = await _http.PostAsJsonAsync("api/churches", church)
            .EnsureSuccess<ChurchDto>();
        await GetChurches();
        return result;
    }

    public async Task<Result<ChurchDto>> UpdateChurch(int churchId, ChurchCreateDto church)
    {
        var result = await _http.PutAsJsonAsync($"api/churches/{churchId}", church)
            .EnsureSuccess<ChurchDto>();
        await GetChurches();
        return result;
    }

    public async Task<Result<bool>> DeleteChurch(int id)
    {
        var result = await _http.DeleteAsync($"api/churches/{id}")
            .EnsureSuccess<bool>();
        await GetChurches();
        return result;
    }
}