namespace Gbs.Client.Wasm.Services.Api.StudentService;

public interface IStudentService
{
    List<StudentDto> Students { get; set; }
    event Action StudentsChanged;
    Task FetchStudents();
    Task<Result<StudentDto>> GetStudentById(int id);
    Task<Result<List<StudentDto>>> AddStudent(StudentCreateDto student);
    Task<Result<List<StudentDto>>> UpdateStudent(int studentId, StudentCreateDto student);
}