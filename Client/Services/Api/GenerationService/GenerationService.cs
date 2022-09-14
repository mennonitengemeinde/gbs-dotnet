namespace gbs.Client.Services.Api.GenerationService;

public class GenerationApiService : BaseApiService, IGenerationService
{
    private readonly HttpClient _http;
    private readonly IUiService _uiService;
    public List<Generation> Generations { get; set; } = new List<Generation>();
    public event Action? GenerationsChanged;

    public GenerationApiService(HttpClient http, IUiService uiService)
    {
        _http = http;
        _uiService = uiService;
    }
    
    public async Task LoadGenerations()
    {
        var result = await GetGenerations();
        if (result.Success == false)
        {
            _uiService.AddErrorAlert(result.Message);
            Generations = new List<Generation>();
            return;;
        }
        Generations = result.Data!;
        Console.WriteLine($"Loaded {Generations.Count} generations");
        GenerationsChanged?.Invoke();
    }

    public async Task<ServiceResponse<List<Generation>>> GetGenerations()
    {
        var response = await _http.GetAsync("api/generations");
        return await EnsureSuccess<List<Generation>>(response);
    }

    public async Task<ServiceResponse<Generation>> AddGeneration(CreateGenerationDto generation)
    {
        var response = await _http.PostAsJsonAsync("api/generations", generation);
        return await EnsureSuccess<Generation>(response);
    }

    public async Task<ServiceResponse<Generation>> UpdateGeneration(int generationId, UpdateGenerationDto generation)
    {
        var response = await _http.PutAsJsonAsync($"api/generations/{generationId}", generation);
        return await EnsureSuccess<Generation>(response);
    }

    public async Task<ServiceResponse<bool>> DeleteGeneration(int generationId)
    {
        var response = await _http.DeleteAsync($"api/generations/{generationId}");
        return await EnsureSuccess<bool>(response);
    }
}