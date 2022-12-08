namespace Gbs.Application.Features.Grades;

public class GradeMapping : Profile
{
    public GradeMapping()
    {
        CreateMap<Grade, GradeResponse>()
            .ForMember(dest => dest.StudentFirstName, opt => opt.MapFrom(src => src.Student.FirstName))
            .ForMember(dest => dest.StudentLastName, opt => opt.MapFrom(src => src.Student.LastName))
            .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Subject.Name));
    }
}