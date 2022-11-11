using Gbs.Wasm.Services.UiService;

namespace Gbs.Wasm.Services.Api.GenerationService;

public class GenerationService : IGenerationService
{
    private readonly HttpClient _http;
    private readonly IUiService _uiService;
    public List<GenerationDto> Generations { get; set; } = new();
    public event Action? GenerationsChanged;

    public GenerationService(HttpClient http, IUiService uiService)
    {
        _http = http;
        _uiService = uiService;
    }

    public async Task LoadGenerations()
    {
        var result = await GetGenerations();
        if (result.Success == false)
        {
            await _uiService.ShowErrorAlert(result.Message, result.StatusCode);
            Generations = new List<GenerationDto>();
            return;
        }

        Generations = result.Data;
        GenerationsChanged?.Invoke();
    }

    public async Task<Result<List<GenerationDto>>> GetGenerations()
    {
        return await _http.GetAsync("api/generations")
            .EnsureSuccess<List<GenerationDto>>();
    }
    
    public async Task<Result<GenerationDto>> GetGeneration(int id)
    {
        return await _http.GetAsync($"api/generations/{id}")
            .EnsureSuccess<GenerationDto>();
    }

    public async Task<Result<GenerationDto>> AddGeneration(GenerationCreateDto generation)
    {
        return await _http.PostAsJsonAsync("api/generations", generation)
            .EnsureSuccess<GenerationDto>();
    }

    public async Task<Result<GenerationDto>> UpdateGeneration(int generationId, GenerationCreateDto generation)
    {
        return await _http.PutAsJsonAsync($"api/generations/{generationId}", generation)
            .EnsureSuccess<GenerationDto>();
    }

    public async Task<Result<bool>> DeleteGeneration(int generationId)
    {
        return await _http.DeleteAsync($"api/generations/{generationId}")
            .EnsureSuccess<bool>();
    }
}