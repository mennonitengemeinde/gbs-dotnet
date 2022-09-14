namespace gbs.Client.Services.Api.GenerationService;

public class GenerationService : IGenerationService
{
    private readonly HttpClient _http;
    public List<Generation> Generations { get; set; } = new List<Generation>();
    public event Action? GenerationsChanged;

    public GenerationService(HttpClient http)
    {
        _http = http;
    }
    
    public async Task LoadGenerations()
    {
        Generations = await GetGenerations();
        GenerationsChanged?.Invoke();
    }

    public async Task<List<Generation>> GetGenerations()
    {
        var result = await _http.GetFromJsonAsync<ServiceResponse<List<Generation>>>("api/generations");
        if (result?.Data == null)
        {
            throw new Exception(result?.Message);
        }

        return result.Data;
    }

    public async Task<Generation> AddGeneration(CreateGenerationDto generation)
    {
        var response = await _http.PostAsJsonAsync("api/generations", generation);
        var result = await response.Content.ReadFromJsonAsync<ServiceResponse<Generation>>();
        if (result?.Data == null)
        {
            throw new Exception(result?.Message);
        }

        return result.Data;
    }

    public async Task<Generation> UpdateGeneration(int generationId, UpdateGenerationDto generation)
    {
        var response = await _http.PutAsJsonAsync($"api/generations/{generationId}", generation);
        var result = await response.Content.ReadFromJsonAsync<ServiceResponse<Generation>>();
        if (result?.Data == null)
        {
            throw new Exception(result?.Message);
        }
        
        return result.Data;
    }

    public async Task DeleteGeneration(int generationId)
    {
        var response = await _http.DeleteAsync($"api/generations/{generationId}");
        var result = await response.Content.ReadFromJsonAsync<ServiceResponse<Generation>>();
        if (result?.Data == null)
        {
            throw new Exception(result?.Message);
        }
    }
}