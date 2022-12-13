namespace Gbs.Application.Features.Lessons;

public class LessonMapping : Profile
{
    public LessonMapping()
    {
        CreateMap<Lesson, LessonResponse>();
        CreateMap<CreateLessonRequest, Lesson>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Generation, opt => opt.Ignore())
            .ForMember(dest => dest.Subject, opt => opt.Ignore())
            .ForMember(dest => dest.Order, opt => opt.Ignore())
            .ForMember(dest => dest.Teacher, opt => opt.Ignore());
        CreateMap<Lesson, SubjectLessonResponse>();
    }
}