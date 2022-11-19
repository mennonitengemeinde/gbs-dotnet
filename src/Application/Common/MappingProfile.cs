namespace Gbs.Application.Common;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Church Mapping
        CreateMap<Church, ChurchDto>()
            .ForMember(dest => dest.StudentCount, opt => opt.MapFrom(src => src.Students.Count));
        CreateMap<ChurchCreateDto, Church>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Students, opt => opt.Ignore());
        
        // Enrollment Mapping
        CreateMap<Enrollment, StudentEnrollmentDto>()
            .ForMember(dest => dest.GenerationName, opt => opt.MapFrom(src => src.Generation.Name));
        CreateMap<Enrollment, GenerationEnrollmentDto>()
            .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.Student.Id))
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.Student.DateOfBirth))
            .ForMember(dest => dest.MaritalStatus, opt => opt.MapFrom(src => src.Student.MaritalStatus))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Student.Email))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Student.Phone))
            .ForMember(dest => dest.ChurchId, opt => opt.MapFrom(src => src.Student.ChurchId))
            .ForMember(dest => dest.ChurchName, opt => opt.MapFrom(src => src.Student.Church.Name));
        
        // Generation Mapping
        CreateMap<Generation, GenerationDto>();
            // .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.Enrollments));
        
        // Grade Mapping
        CreateMap<Grade, GradeDto>()
            .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Subject.Name));
        
        // Lesson Mapping
        CreateMap<Lesson, LessonDto>();
        CreateMap<LessonCreateDto, Lesson>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Generation, opt => opt.Ignore())
            .ForMember(dest => dest.Subject, opt => opt.Ignore())
            .ForMember(dest => dest.Teacher, opt => opt.Ignore());
        CreateMap<Lesson, SubjectLessonDto>();
        
        // Livestream Mapping
        CreateMap<LiveStream, StreamDto>()
            .ForMember(dest => dest.Teachers, opt => opt.MapFrom(src => src.StreamTeachers));
        CreateMap<StreamCreateDto, LiveStream>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Generation, opt => opt.Ignore())
            .ForMember(dest => dest.StreamTeachers, opt => opt.Ignore());
        CreateMap<LiveStreamTeacher, TeacherDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Teacher.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Teacher.Name))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Teacher.UserId));
        
        // Student Mapping
        CreateMap<Student, StudentDto>()
            .ForMember(dest => dest.ChurchName, opt => opt.MapFrom(src => src.Church.Name));
        CreateMap<StudentCreateDto, Student>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Church, opt => opt.Ignore())
            .ForMember(dest => dest.Enrollments, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore());
        
        // Subject Mapping
        CreateMap<Subject, SubjectDto>();
        CreateMap<SubjectCreateDto, Subject>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Lessons, opt => opt.Ignore())
            .ForMember(dest => dest.Questions, opt => opt.Ignore());
        
        // Teacher Mapping
        CreateMap<Teacher, TeacherDto>();
    }
}