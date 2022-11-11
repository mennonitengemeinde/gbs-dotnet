namespace Gbs.Application.Common.Interfaces.Repositories;

public interface ITeacherRepository
{
    Task<Result<List<TeacherDto>>> GetTeachers();
    Task<Result<TeacherDto>> GetTeacherById(int id);
    Task<Result<TeacherDto>> AddTeacher(TeacherCreateDto teacherCreateDto);
    Task<Result<TeacherDto>> UpdateTeacher(int teacherId, TeacherCreateDto teacherDto);
}