namespace gbs.Server.Repository.StudentRepository;

public interface IStudentRepository
{
    Task<ServiceResponse<List<StudentDto>>> GetStudents();
    Task<ServiceResponse<StudentDto>> GetStudentById(int id);
    Task<ServiceResponse<List<StudentDto>>> AddStudent(IStudentCreateDto studentDto);
    Task<ServiceResponse<List<StudentDto>>> UpdateStudent(int studentId, IStudentCreateDto studentDto);
}