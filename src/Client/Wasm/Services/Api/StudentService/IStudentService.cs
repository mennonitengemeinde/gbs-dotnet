namespace gbs.Client.Wasm.Services.Api.StudentService;

public interface IStudentService
{
    List<StudentDto> Students { get; set; }
    event Action StudentsChanged;
    Task FetchStudents();
    Task<Result<StudentDto>> GetStudentById(int id);
    Task<Result<StudentDto>> AddStudent(StudentCreateDto student);
    Task<Result<StudentDto>> UpdateStudent(int studentId, StudentCreateDto student);
}