namespace gbs.Client.Services.Api.ChurchService;

public class ChurchService : IChurchService
{
    private readonly HttpClient _http;
    private readonly IUiService _uiService;
    public List<Church> Churches { get; set; } = new List<Church>();
    public event Action? ChurchesChanged;

    public ChurchService(HttpClient http, IUiService uiService)
    {
        _http = http;
        _uiService = uiService;
    }

    private async Task UpdateChurches(ServiceResponse<List<Church>> response)
    {
        if (!response.Success)
        {
            await _uiService.ShowErrorAlert(response.Message, response.StatusCode);
            Churches = new List<Church>();
            return;
        }

        Churches = response.Data;
        ChurchesChanged?.Invoke();
    }

    public async Task<ServiceResponse<List<Church>>> GetChurches()
    {
        var result = await _http.GetAsync("api/churches")
            .EnsureSuccess<List<Church>>();
        await UpdateChurches(result);
        return result;
    }

    public async Task<ServiceResponse<Church>> GetChurch(int id)
    {
        return await _http.GetAsync($"api/churches/{id}")
            .EnsureSuccess<Church>();
    }

    public async Task<ServiceResponse<List<Church>>> AddChurch(ChurchCreateDto church)
    {
        var result = await _http.PostAsJsonAsync("api/churches", church)
            .EnsureSuccess<List<Church>>();
        await UpdateChurches(result);
        return result;
    }

    public async Task<ServiceResponse<List<Church>>> UpdateChurch(int churchId, ChurchCreateDto church)
    {
        var result = await _http.PutAsJsonAsync($"api/churches/{churchId}", church)
            .EnsureSuccess<List<Church>>();
        await UpdateChurches(result);
        return result;
    }

    public async Task<ServiceResponse<List<Church>>> DeleteChurch(int id)
    {
        var result = await _http.DeleteAsync($"api/churches/{id}")
            .EnsureSuccess<List<Church>>();
        await UpdateChurches(result);
        return result;
    }
}