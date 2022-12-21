using System.Runtime.Serialization;
using Gbs.Application.Entities;
using Gbs.Application.Features.Churches;
using Gbs.Application.Features.Generations;
using Gbs.Application.Features.Grades;
using Gbs.Application.Features.GradeTypes;
using Gbs.Application.Features.Identity;
using Gbs.Application.Features.Lessons;
using Gbs.Application.Features.Streams;
using Gbs.Application.Features.Students;
using Gbs.Application.Features.Subjects;
using Gbs.Application.Features.Teachers;
using Gbs.Shared.Churches;
using Gbs.Shared.Generations;
using Gbs.Shared.Grades;
using Gbs.Shared.GradeTypes;
using Gbs.Shared.Identity;
using Gbs.Shared.Lessons;
using Gbs.Shared.Streams;
using Gbs.Shared.Students;
using Gbs.Shared.Subjects;
using Gbs.Shared.Teachers;

namespace Gbs.Tests.Application.UnitTests.Common;

public class MappingTests
{
    [Fact]
    public void ShouldHaveValidConfiguration()
    {
        var configuration = new MapperConfiguration(config =>
        {
            config.AddProfile<ChurchMapping>();
            config.AddProfile<GenerationMapping>();
            config.AddProfile<GradeMapping>();
            config.AddProfile<GradeTypeMapping>();
            config.AddProfile<IdentityMapping>();
            config.AddProfile<LessonMapping>();
            config.AddProfile<StreamMapping>();
            config.AddProfile<StudentMapping>();
            config.AddProfile<SubjectMapping>();
            config.AddProfile<TeacherMapping>();
        });
        
        var mapper = configuration.CreateMapper();
        
        // configuration.AssertConfigurationIsValid();
        mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }
    
    [Theory]
    [InlineData(typeof(Church), typeof(ChurchResponse))]
    [InlineData(typeof(CreateChurchRequest), typeof(Church))]
    [InlineData(typeof(Student), typeof(StudentResponse))]
    [InlineData(typeof(Student), typeof(GenerationStudentResponse))]
    [InlineData(typeof(Generation), typeof(GenerationResponse))]
    [InlineData(typeof(Grade), typeof(GradeResponse))]
    [InlineData(typeof(GradeType), typeof(GradeTypeResponse))]
    [InlineData(typeof(CreateGradeTypeRequest), typeof(GradeType))]
    [InlineData(typeof(Lesson), typeof(LessonResponse))]
    [InlineData(typeof(CreateLessonRequest), typeof(Lesson))]
    [InlineData(typeof(Lesson), typeof(SubjectLessonResponse))]
    [InlineData(typeof(LiveStream), typeof(StreamResponse))]
    [InlineData(typeof(LiveStreamTeacher), typeof(TeacherResponse))]
    [InlineData(typeof(CreateStreamRequest), typeof(LiveStream))]
    [InlineData(typeof(CreateStudentRequest), typeof(Student))]
    [InlineData(typeof(Subject), typeof(SubjectResponse))]
    [InlineData(typeof(CreateSubjectRequest), typeof(Subject))]
    [InlineData(typeof(Teacher), typeof(TeacherResponse))]
    [InlineData(typeof(User), typeof(UserResponse))]
    [InlineData(typeof(RegisterRequest), typeof(User))]
    [InlineData(typeof(UserRole), typeof(string))]
    public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
    {
        var configuration = new MapperConfiguration(config =>
        {
            config.AddProfile<ChurchMapping>();
            config.AddProfile<GenerationMapping>();
            config.AddProfile<GradeMapping>();
            config.AddProfile<GradeTypeMapping>();
            config.AddProfile<IdentityMapping>();
            config.AddProfile<LessonMapping>();
            config.AddProfile<StreamMapping>();
            config.AddProfile<StudentMapping>();
            config.AddProfile<SubjectMapping>();
            config.AddProfile<TeacherMapping>();
        });
        
        var mapper = configuration.CreateMapper();
        
        var instance = GetInstanceOf(source);
    
        mapper.Map(instance, source, destination);
    }
    
    private object GetInstanceOf(Type type)
    {
        if (type.GetConstructor(Type.EmptyTypes) != null)
            return Activator.CreateInstance(type)!;
    
        // Type without parameterless constructor
        return FormatterServices.GetUninitializedObject(type);
    }
}