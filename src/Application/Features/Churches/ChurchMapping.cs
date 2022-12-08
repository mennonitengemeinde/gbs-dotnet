namespace Gbs.Application.Features.Churches;

public class ChurchMapping : Profile
{
    public ChurchMapping()
    {
        CreateMap<Church, ChurchResponse>()
            .ForMember(dest => dest.StudentCount, opt => opt.MapFrom(src => src.Students.Count));
        CreateMap<CreateChurchRequest, Church>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Students, opt => opt.Ignore());
    }
}