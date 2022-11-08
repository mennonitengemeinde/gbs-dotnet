namespace gbs.Client.Wasm.Services.Api.StudentService;

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

    public async Task<Result<StudentDto>> GetStudentById(int id)
    {
        var student = await _http.GetAsync($"api/students/{id}")
            .EnsureSuccess<StudentDto>();
        return student;
    }

    public async Task<Result<StudentDto>> AddStudent(StudentCreateDto student)
    {
        var result = await _http.PostAsJsonAsync("api/students", student)
            .EnsureSuccess<List<StudentDto>>();
        return await UpdateRepository(result);
    }

    public async Task<Result<StudentDto>> UpdateStudent(int studentId, StudentCreateDto student)
    {
        var result = await _http.PutAsJsonAsync($"api/students/{studentId}", student)
            .EnsureSuccess<List<StudentDto>>();
        return await UpdateRepository(result);
    }

    private async Task<Result<StudentDto>> UpdateRepository(Result<List<StudentDto>> response)
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
        
        return new Result<StudentDto> { Success = response.Success, Message = response.Message };
    }
}