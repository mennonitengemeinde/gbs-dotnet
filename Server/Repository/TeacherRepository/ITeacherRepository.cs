namespace gbs.Server.Repository.TeacherRepository;

public interface ITeacherRepository
{
    Task<ServiceResponse<List<Teacher>>> GetTeachers();
    Task<ServiceResponse<Teacher>> AddTeacher(TeacherCreateDto teacherCreateDto);
}