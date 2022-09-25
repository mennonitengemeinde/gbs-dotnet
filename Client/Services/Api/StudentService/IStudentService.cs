namespace gbs.Client.Services.Api.StudentService;

public interface IStudentService
{
    List<StudentDto> Students { get; set; }
    event Action StudentsChanged;
    Task FetchStudents();
    Task AddStudent(IStudentCreateDto student);
}