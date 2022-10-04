namespace gbs.Client.Services.Api.StudentService;

public interface IStudentService
{
    List<StudentDto> Students { get; set; }
    event Action StudentsChanged;
    Task FetchStudents();
    Task<ServiceResponse<StudentDto>> GetStudentById(int id);
    Task<ServiceResponse<StudentDto>> AddStudent(IStudentCreateDto student);
    Task<ServiceResponse<StudentDto>> UpdateStudent(int studentId, IStudentCreateDto student);
}