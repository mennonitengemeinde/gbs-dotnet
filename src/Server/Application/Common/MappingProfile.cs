using AutoMapper;
using Gbs.Core.Domain.Entities;

namespace Gbs.Server.Application.Common;

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
        
        // Student Mapping
        CreateMap<Student, StudentDto>()
            .ForMember(dest => dest.ChurchName, opt => opt.MapFrom(src => src.Church.Name));
        CreateMap<StudentCreateDto, Student>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Church, opt => opt.Ignore())
            .ForMember(dest => dest.Enrollments, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore());
        
        // Teacher Mapping
        CreateMap<Teacher, TeacherDto>();
    }
}