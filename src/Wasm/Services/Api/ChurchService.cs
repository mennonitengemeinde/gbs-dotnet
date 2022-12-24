namespace Gbs.Wasm.Services.Api;

public class ChurchService
{
    private readonly HttpClient _http;

    public ChurchService(HttpClient http)
    {
        _http = http;
    }
    
    public async Task<Result<List<ChurchResponse>>> GetAll()
    {
        return await _http.GetAsync("api/churches")
            .EnsureSuccess<List<ChurchResponse>>();
    }
}