namespace Gbs.Application.Features.Subjects;

public class SubjectMapping : Profile
{
    public SubjectMapping()
    {
        CreateMap<Subject, SubjectResponse>();
        CreateMap<CreateSubjectRequest, Subject>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Lessons, opt => opt.Ignore())
            .ForMember(dest => dest.Questions, opt => opt.Ignore());
    }
}