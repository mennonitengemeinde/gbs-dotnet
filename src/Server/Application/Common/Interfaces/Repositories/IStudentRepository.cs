namespace Gbs.Server.Application.Common.Interfaces.Repositories;

public interface IStudentRepository
{
    Task<Result<List<StudentDto>>> GetStudents();
    Task<Result<StudentDto>> GetStudentById(int id);
    Task<Result<List<StudentDto>>> AddStudent(StudentCreateDto studentDto);
    Task<Result<List<StudentDto>>> UpdateStudent(int studentId, StudentCreateDto studentDto);
}