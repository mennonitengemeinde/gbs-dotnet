using System.Runtime.Serialization;
using Gbs.Domain.Entities;
using Gbs.Shared.Churches;
using Gbs.Shared.Generations;
using Gbs.Shared.Grades;
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
            config.AddProfile<MappingProfile>());
        
        var mapper = configuration.CreateMapper();
        
        // configuration.AssertConfigurationIsValid();
        mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }
    
    [Theory]
    [InlineData(typeof(Church), typeof(ChurchResponse))]
    [InlineData(typeof(ChurchCreateDto), typeof(Church))]
    [InlineData(typeof(Student), typeof(StudentDto))]
    [InlineData(typeof(Generation), typeof(GenerationDto))]
    [InlineData(typeof(Grade), typeof(GradeDto))]
    [InlineData(typeof(Lesson), typeof(LessonDto))]
    [InlineData(typeof(LessonCreateDto), typeof(Lesson))]
    [InlineData(typeof(Lesson), typeof(SubjectLessonDto))]
    [InlineData(typeof(LiveStream), typeof(StreamDto))]
    [InlineData(typeof(LiveStreamTeacher), typeof(TeacherDto))]
    [InlineData(typeof(StreamCreateDto), typeof(LiveStream))]
    [InlineData(typeof(StudentCreateDto), typeof(Student))]
    [InlineData(typeof(Subject), typeof(SubjectDto))]
    [InlineData(typeof(SubjectCreateDto), typeof(Subject))]
    [InlineData(typeof(Teacher), typeof(TeacherDto))]
    [InlineData(typeof(User), typeof(UserDto))]
    [InlineData(typeof(RegisterDto), typeof(User))]
    [InlineData(typeof(UserRole), typeof(string))]
    public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
    {
        var configuration = new MapperConfiguration(config => 
            config.AddProfile<MappingProfile>());
        
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