using Gbs.Shared.Dto.Subjects;

namespace Gbs.Wasm.Services.Api.SubjectService;

public class SubjectService : ISubjectService
{
    private readonly HttpClient _http;
    private readonly IUiService _uiService;

    public SubjectService(HttpClient http, IUiService uiService)
    {
        _http = http;
        _uiService = uiService;
    }

    public List<SubjectDto> Subjects { get; set; } = new();
    public event Action? SubjectsChanged;

    public async Task<Result<List<SubjectDto>>> FetchSubjects()
    {
        var result = await _http.GetAsync(SubjectRoutes.GetAll)
            .EnsureSuccess<List<SubjectDto>>();
        
        if (!result.Success)
        {
            await _uiService.ShowErrorAlert(result.Message, result.StatusCode);
            Subjects = new List<SubjectDto>();
        }

        Subjects = result.Data ?? new List<SubjectDto>();
        SubjectsChanged?.Invoke();
        
        return result;
    }

    public async Task<Result<SubjectDto>> FetchSubject(int id)
    {
        var result = await _http.GetAsync(SubjectRoutes.Get(id))
            .EnsureSuccess<SubjectDto>();
        return result;
    }

    public async Task<Result<SubjectDto>> AddSubject(SubjectCreateDto subject)
    {
        var result = await _http.PostAsJsonAsync(SubjectRoutes.Create, subject)
            .EnsureSuccess<SubjectDto>();
        await FetchSubjects();
        return result;
    }
}