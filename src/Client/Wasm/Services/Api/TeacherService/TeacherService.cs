namespace gbs.Client.Wasm.Services.Api.TeacherService;

public class TeacherService : ITeacherService
{
    private readonly HttpClient _http;
    private readonly IUiService _uiService;
    public List<Teacher> Teachers { get; set; } = new List<Teacher>();
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
            Teachers = new List<Teacher>();
            return;
        }

        Teachers = result.Data;
        TeachersChanged?.Invoke();
    }

    public async Task<ServiceResponse<List<Teacher>>> FetchTeachers()
    {
        return await _http.GetAsync("api/teachers")
            .EnsureSuccess<List<Teacher>>();
    }

    public async Task<ServiceResponse<Teacher>> FetchTeacher(int id)
    {
        return await _http.GetAsync($"api/teachers/{id}")
            .EnsureSuccess<Teacher>();
    }

    public async Task<ServiceResponse<Teacher>> AddTeacher(TeacherCreateDto teacher)
    {
        return await _http.PostAsJsonAsync("api/teachers", teacher)
            .EnsureSuccess<Teacher>();
    }

    public async Task<ServiceResponse<Teacher>> UpdateTeacher(int teacherId, TeacherCreateDto teacher)
    {
        return await _http.PutAsJsonAsync($"api/teachers/{teacherId}", teacher)
            .EnsureSuccess<Teacher>();
    }
}