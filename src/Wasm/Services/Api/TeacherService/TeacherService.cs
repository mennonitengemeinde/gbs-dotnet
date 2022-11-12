using Gbs.Wasm.Extensions;
using Gbs.Wasm.Services.UiService;

namespace Gbs.Wasm.Services.Api.TeacherService;

public class TeacherService : ITeacherService
{
    private readonly HttpClient _http;
    private readonly IUiService _uiService;
    public List<TeacherDto> Teachers { get; set; } = new();
    public event Action? TeachersChanged;

    public TeacherService(HttpClient http, IUiService uiService)
    {
        _http = http;
        _uiService = uiService;
    }

    public async Task LoadTeachers()
    {
        var result = await FetchTeachers();
        if (!result.Success)
        {
            await _uiService.ShowErrorAlert(result.Message, result.StatusCode);
            Teachers = new List<TeacherDto>();
            return;
        }

        Teachers = result.Data ?? new List<TeacherDto>();
        TeachersChanged?.Invoke();
    }

    public async Task<Result<List<TeacherDto>>> FetchTeachers()
    {
        return await _http.GetAsync("api/teachers")
            .EnsureSuccess<List<TeacherDto>>();
    }

    public async Task<Result<TeacherDto>> FetchTeacher(int id)
    {
        return await _http.GetAsync($"api/teachers/{id}")
            .EnsureSuccess<TeacherDto>();
    }

    public async Task<Result<TeacherDto>> AddTeacher(TeacherCreateDto teacher)
    {
        return await _http.PostAsJsonAsync("api/teachers", teacher)
            .EnsureSuccess<TeacherDto>();
    }

    public async Task<Result<TeacherDto>> UpdateTeacher(int teacherId, TeacherCreateDto teacher)
    {
        return await _http.PutAsJsonAsync($"api/teachers/{teacherId}", teacher)
            .EnsureSuccess<TeacherDto>();
    }
}