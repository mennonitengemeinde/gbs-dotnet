namespace gbs.Client.Wasm.Services.Api.TeacherService;

public interface ITeacherService
{
    List<Teacher> Teachers { get; set; }
    event Action TeachersChanged;
    Task LoadTeachers();
    Task<ServiceResponse<List<Teacher>>> FetchTeachers();
    Task<ServiceResponse<Teacher>> FetchTeacher(int id);
    Task<ServiceResponse<Teacher>> AddTeacher(TeacherCreateDto teacher);
    Task<ServiceResponse<Teacher>> UpdateTeacher(int teacherId, TeacherCreateDto teacher);
}