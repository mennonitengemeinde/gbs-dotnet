using Gbs.Shared.Generations;
using Gbs.Shared.Grades;
using Gbs.Shared.Identity;
using Gbs.Shared.Lessons;
using Gbs.Shared.Streams;
using Gbs.Shared.Students;
using Gbs.Shared.Subjects;
using Gbs.Shared.Teachers;

namespace Gbs.Application.Common;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Church Mapping
        CreateMap<Church, ChurchResponse>()
            .ForMember(dest => dest.StudentCount, opt => opt.MapFrom(src => src.Students.Count));
        CreateMap<ChurchCreateDto, Church>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Students, opt => opt.Ignore());
        
        // Generation Mapping
        CreateMap<Generation, GenerationDto>();
            // .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.Enrollments));
        
        // Grade Mapping
        CreateMap<Grade, GradeDto>()
            .ForMember(dest => dest.StudentFirstName, opt => opt.MapFrom(src => src.Student.FirstName))
            .ForMember(dest => dest.StudentLastName, opt => opt.MapFrom(src => src.Student.LastName))
            .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Subject.Name));
        
        // Lesson Mapping
        CreateMap<Lesson, LessonDto>();
        CreateMap<LessonCreateDto, Lesson>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Generation, opt => opt.Ignore())
            .ForMember(dest => dest.Subject, opt => opt.Ignore())
            .ForMember(dest => dest.Order, opt => opt.Ignore())
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
            .ForMember(dest => dest.EnrollmentStatus, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Generation, opt => opt.Ignore())
            .ForMember(dest => dest.Church, opt => opt.Ignore())
            .ForMember(dest => dest.Grades, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore());
        
        // Subject Mapping
        CreateMap<Subject, SubjectDto>();
        CreateMap<SubjectCreateDto, Subject>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Lessons, opt => opt.Ignore())
            .ForMember(dest => dest.Questions, opt => opt.Ignore());
        
        // Teacher Mapping
        CreateMap<Teacher, TeacherDto>();
        
        // User Mapping
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles.Select(ur => ur.Role.Name)));
        CreateMap<RegisterDto, User>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.IsActive, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.LastLogin, opt => opt.Ignore())
            .ForMember(dest => dest.TeacherId, opt => opt.Ignore())
            .ForMember(dest => dest.Teacher, opt => opt.Ignore())
            .ForMember(dest => dest.ChurchId, opt => opt.Ignore())
            .ForMember(dest => dest.Church, opt => opt.Ignore())
            .ForMember(dest => dest.UserRoles, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.NormalizedUserName, opt => opt.Ignore())
            .ForMember(dest => dest.NormalizedEmail, opt => opt.Ignore())
            .ForMember(dest => dest.EmailConfirmed, opt => opt.Ignore())
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            .ForMember(dest => dest.SecurityStamp, opt => opt.Ignore())
            .ForMember(dest => dest.ConcurrencyStamp, opt => opt.Ignore())
            .ForMember(dest => dest.PhoneNumber, opt => opt.Ignore())
            .ForMember(dest => dest.PhoneNumberConfirmed, opt => opt.Ignore())
            .ForMember(dest => dest.TwoFactorEnabled, opt => opt.Ignore())
            .ForMember(dest => dest.LockoutEnd, opt => opt.Ignore())
            .ForMember(dest => dest.LockoutEnabled, opt => opt.Ignore())
            .ForMember(dest => dest.AccessFailedCount, opt => opt.Ignore());

        // User Role Mapping
    }
}