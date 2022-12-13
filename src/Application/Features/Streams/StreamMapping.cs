namespace Gbs.Application.Features.Streams;

public class StreamMapping : Profile
{
    public StreamMapping()
    {
        CreateMap<LiveStream, StreamResponse>()
            .ForMember(dest => dest.Teachers, opt => opt.MapFrom(src => src.StreamTeachers));
        CreateMap<CreateStreamRequest, LiveStream>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Generation, opt => opt.Ignore())
            .ForMember(dest => dest.StreamTeachers, opt => opt.Ignore());
        CreateMap<LiveStreamTeacher, TeacherResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Teacher.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Teacher.Name))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Teacher.UserId));
    }
}