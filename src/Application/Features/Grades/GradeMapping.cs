namespace Gbs.Application.Features.Grades;

public class GradeMapping : Profile
{
    public GradeMapping()
    {
        CreateMap<Grade, GradeResponse>()
            .ForMember(dest => dest.StudentFirstName, opt => opt.MapFrom(src => src.Student.FirstName))
            .ForMember(dest => dest.StudentLastName, opt => opt.MapFrom(src => src.Student.LastName))
            .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Subject.Name))
            .ForMember(dest => dest.GradeType, opt => opt.MapFrom(src => src.GradeType.Name));
        
        CreateMap<CreateGradeRequest, Grade>()
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.Date ?? DateTime.Now)))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.GradeType, opt => opt.Ignore())
            .ForMember(dest => dest.Student, opt => opt.Ignore())
            .ForMember(dest => dest.Subject, opt => opt.Ignore());
    }
}