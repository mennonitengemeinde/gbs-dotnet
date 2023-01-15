namespace Gbs.Application.Features.Lessons;

public class LessonMapping : Profile
{
    public LessonMapping()
    {
        string? currentUserId = null;
        CreateMap<Lesson, LessonResponse>()
            .ForMember(dest => dest.HasWatched,
                opt => opt.MapFrom(src =>
                    src.UsersWatched.Any(x => x.LessonId == src.Id && x.UserId == currentUserId)));
        CreateMap<CreateLessonRequest, Lesson>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Generation, opt => opt.Ignore())
            .ForMember(dest => dest.Subject, opt => opt.Ignore())
            .ForMember(dest => dest.Order, opt => opt.Ignore())
            .ForMember(dest => dest.Teacher, opt => opt.Ignore())
            .ForMember(dest => dest.UsersWatched, opt => opt.Ignore());
        CreateMap<Lesson, SubjectLessonResponse>();
    }
}