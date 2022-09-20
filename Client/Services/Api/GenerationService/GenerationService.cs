namespace gbs.Client.Services.Api.GenerationService;

public class GenerationService : IGenerationService
{
    private readonly HttpClient _http;
    private readonly IUiService _uiService;
    public List<Generation> Generations { get; set; } = new List<Generation>();
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
            _uiService.ShowErrorAlert(result.Message);
            Generations = new List<Generation>();
            return;
            ;
        }

        Generations = result.Data!;
        GenerationsChanged?.Invoke();
    }

    public async Task<ServiceResponse<List<Generation>>> GetGenerations()
    {
        return await _http.GetAsync("api/generations")
            .EnsureSuccess<List<Generation>>();
    }

    public async Task<ServiceResponse<Generation>> AddGeneration(GenerationCreateDto generation)
    {
        return await _http.PostAsJsonAsync("api/generations", generation)
            .EnsureSuccess<Generation>();
    }

    public async Task<ServiceResponse<Generation>> UpdateGeneration(int generationId, GenerationUpdateDto generation)
    {
        return await _http.PutAsJsonAsync($"api/generations/{generationId}", generation)
            .EnsureSuccess<Generation>();
    }

    public async Task<ServiceResponse<bool>> DeleteGeneration(int generationId)
    {
        return await _http.DeleteAsync($"api/generations/{generationId}")
            .EnsureSuccess<bool>();
    }
}