namespace gbs.Server.Repository.TeacherRepository;

public interface ITeacherRepository
{
    Task<ServiceResponse<List<Teacher>>> GetTeachers();
    Task<ServiceResponse<Teacher>> GetTeacherById(int id);
    Task<ServiceResponse<Teacher>> AddTeacher(TeacherCreateDto teacherCreateDto);
    Task<ServiceResponse<Teacher>> UpdateTeacher(int teacherId, TeacherCreateDto teacherDto);
}