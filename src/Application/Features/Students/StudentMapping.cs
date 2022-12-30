namespace Gbs.Application.Features.Students;

public class StudentMapping : Profile
{
    public StudentMapping()
    {
        CreateMap<Student, StudentResponse>()
            .ForMember(dest => dest.ChurchName, opt => opt.MapFrom(src => src.Church.Name))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
        CreateMap<Student, GenerationStudentResponse>()
            .ForMember(dest => dest.ChurchName, opt => opt.MapFrom(src => src.Church.Name));
        CreateMap<CreateStudentRequest, Student>()
            .ForMember(dest => dest.EnrollmentStatus, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Generation, opt => opt.Ignore())
            .ForMember(dest => dest.Church, opt => opt.Ignore())
            .ForMember(dest => dest.Grades, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore());
    }
}