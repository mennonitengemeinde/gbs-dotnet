namespace gbs.Server.Repository.StudentRepository;

public interface IStudentRepository
{
    Task<ServiceResponse<List<Student>>> GetStudents();
    Task<ServiceResponse<Student>> GetStudentById(int id);
    Task<ServiceResponse<List<Student>>> AddStudent(StudentCreateDto studentDto);
    Task<ServiceResponse<List<Student>>> UpdateStudent(int studentId, StudentCreateDto studentDto);
}