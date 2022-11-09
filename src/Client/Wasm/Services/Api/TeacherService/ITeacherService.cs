namespace Gbs.Client.Wasm.Services.Api.TeacherService;

public interface ITeacherService
{
    List<TeacherDto> Teachers { get; set; }
    event Action TeachersChanged;
    Task LoadTeachers();
    Task<Result<List<TeacherDto>>> FetchTeachers();
    Task<Result<TeacherDto>> FetchTeacher(int id);
    Task<Result<TeacherDto>> AddTeacher(TeacherCreateDto teacher);
    Task<Result<TeacherDto>> UpdateTeacher(int teacherId, TeacherCreateDto teacher);
}