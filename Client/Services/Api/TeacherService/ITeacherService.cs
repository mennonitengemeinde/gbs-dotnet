namespace gbs.Client.Services.Api.TeacherService;

public interface ITeacherService
{
    List<Teacher> Teachers { get; set; }
    event Action TeachersChanged;
    Task LoadTeachers();
    Task<ServiceResponse<List<Teacher>>> GetTeachers();
    Task<ServiceResponse<Teacher>> AddTeacher(TeacherCreateDto teacher);
}