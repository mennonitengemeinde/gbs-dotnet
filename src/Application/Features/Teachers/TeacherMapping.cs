namespace Gbs.Application.Features.Teachers;

public class TeacherMapping : Profile
{
    public TeacherMapping()
    {
        CreateMap<Teacher, TeacherResponse>();
    }
}