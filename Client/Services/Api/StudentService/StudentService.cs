namespace gbs.Client.Services.Api.StudentService;

public class StudentService : IStudentService
{
    private readonly HttpClient _http;
    private readonly IUiService _uiService;
    public List<StudentDto> Students { get; set; } = new();
    public event Action? StudentsChanged;

    public StudentService(HttpClient http, IUiService uiService)
    {
        _http = http;
        _uiService = uiService;
    }

    public async Task FetchStudents()
    {
        var students = await _http.GetAsync("api/students")
            .EnsureSuccess<List<StudentDto>>();
        await UpdateRepository(students);
    }

    public async Task<ServiceResponse<StudentDto>> GetStudentById(int id)
    {
        var student = await _http.GetAsync($"api/students/{id}")
            .EnsureSuccess<StudentDto>();
        return student;
    }

    public async Task<ServiceResponse<StudentDto>> AddStudent(IStudentCreateDto student)
    {
        var result = await _http.PostAsJsonAsync("api/students", student)
            .EnsureSuccess<List<StudentDto>>();
        return await UpdateRepository(result);
    }

    public async Task<ServiceResponse<StudentDto>> UpdateStudent(int studentId, IStudentCreateDto student)
    {
        var result = await _http.PutAsJsonAsync($"api/students/{studentId}", student)
            .EnsureSuccess<List<StudentDto>>();
        return await UpdateRepository(result);
    }

    private async Task<ServiceResponse<StudentDto>> UpdateRepository(ServiceResponse<List<StudentDto>> response)
    {
        if (response.Success)
        {
            Students = response.Data;
        }
        else
        {
            await _uiService.ShowErrorAlert(response.Message);
            Students = new List<StudentDto>();
        }

        StudentsChanged?.Invoke();
        
        return new ServiceResponse<StudentDto> { Success = response.Success, Message = response.Message };
    }
}