namespace Gbs.Wasm.Services.Api;

public class GradeService : IGradeService
{
    private readonly HttpClient _http;
    private readonly IUiService _uiService;
    private const string BaseUrl = "api/grades";

    public GradeService(HttpClient http, IUiService uiService)
    {
        _http = http;
        _uiService = uiService;
    }

    public ServiceError? Error { get; private set; }
    public event Action? OnChange;

    public void NotifyStateChanged()
    {
        OnChange?.Invoke();
    }

    public async Task Create(CreateGradeRequest request)
    {
        var result = await _http.PostAsJsonAsync(BaseUrl, request)
            .EnsureSuccess<GradeResponse>();
        if (!result.Success)
        {
            SetError(result.Message, result.Errors, result.StatusCode);
            return;
        }
        
        Error = null;
    }

    public async Task Update(CreateGradeRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public void ClearError()
    {
        Error = null;
        NotifyStateChanged();
    }

    private void SetError(string message, IEnumerable<string>? errors, int statusCode)
    {
        Error = new ServiceError
        {
            Message = message,
            Errors = errors?.ToArray(),
            StatusCode = statusCode
        };
        _uiService.ShowErrorAlert(message, statusCode);
        NotifyStateChanged();
    }
}