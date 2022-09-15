namespace gbs.Client.Services.Api.TeacherService;

public class TeacherService : BaseApiService, ITeacherService
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
        var result = await GetTeachers();
        if (result.Success == false || result.Data == null)
        {
            _uiService.AddErrorAlert(result.Message);
            Teachers = new List<Teacher>();
            return;
        }
        Teachers = result.Data;
        TeachersChanged?.Invoke();
    }

    public async Task<ServiceResponse<List<Teacher>>> GetTeachers()
    {
        var response = await _http.GetAsync("api/teachers");
        return await EnsureSuccess<List<Teacher>>(response);
    }

    public async Task<ServiceResponse<Teacher>> AddTeacher(TeacherCreateDto teacher)
    {
        var response = await _http.PostAsJsonAsync("api/teachers", teacher);
        return await EnsureSuccess<Teacher>(response);
    }
}