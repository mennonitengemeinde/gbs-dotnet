namespace gbs.Client.Services.Api.ChurchService;

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

    private async Task UpdateChurches(ServiceResponse<List<ChurchDto>> response)
    {
        if (!response.Success)
        {
            await _uiService.ShowErrorAlert(response.Message, response.StatusCode);
            Churches = new List<ChurchDto>();
            return;
        }

        Churches = response.Data;
        ChurchesChanged?.Invoke();
    }

    public async Task<ServiceResponse<List<ChurchDto>>> GetChurches()
    {
        var result = await _http.GetAsync("api/churches")
            .EnsureSuccess<List<ChurchDto>>();
        await UpdateChurches(result);
        return result;
    }

    public async Task<ServiceResponse<ChurchDto>> GetChurch(int id)
    {
        return await _http.GetAsync($"api/churches/{id}")
            .EnsureSuccess<ChurchDto>();
    }

    public async Task<ServiceResponse<List<ChurchDto>>> AddChurch(ChurchCreateDto church)
    {
        var result = await _http.PostAsJsonAsync("api/churches", church)
            .EnsureSuccess<List<ChurchDto>>();
        await UpdateChurches(result);
        return result;
    }

    public async Task<ServiceResponse<List<ChurchDto>>> UpdateChurch(int churchId, ChurchCreateDto church)
    {
        var result = await _http.PutAsJsonAsync($"api/churches/{churchId}", church)
            .EnsureSuccess<List<ChurchDto>>();
        await UpdateChurches(result);
        return result;
    }

    public async Task<ServiceResponse<List<ChurchDto>>> DeleteChurch(int id)
    {
        var result = await _http.DeleteAsync($"api/churches/{id}")
            .EnsureSuccess<List<ChurchDto>>();
        await UpdateChurches(result);
        return result;
    }
}