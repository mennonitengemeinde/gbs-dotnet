namespace Gbs.Application.Churches;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<Church, ChurchDto>()
            .ForMember(dest => dest.StudentCount, opt => opt.MapFrom(src => src.Students.Count));
        CreateMap<CreateChurchRequest, Church>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Students, opt => opt.Ignore());
    }
}