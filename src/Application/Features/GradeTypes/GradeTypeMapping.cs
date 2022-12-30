using Gbs.Shared.GradeTypes;

namespace Gbs.Application.Features.GradeTypes;

public class GradeTypeMapping : Profile
{
    public GradeTypeMapping()
    {
        CreateMap<GradeType, GradeTypeResponse>();
        CreateMap<CreateGradeTypeRequest, GradeType>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Generation, opt => opt.Ignore())
            .ForMember(dest => dest.Grades, opt => opt.Ignore());
    }
}