using System.Runtime.Serialization;
using AutoMapper;
using Gbs.Core.Domain.Dto.Churches;
using Gbs.Core.Domain.Dto.Generations;
using Gbs.Core.Domain.Dto.Grades;
using Gbs.Core.Domain.Dto.Students;
using Gbs.Core.Domain.Dto.Teachers;
using Gbs.Core.Domain.Entities;
using Gbs.Server.Application.Common;

namespace gbs.Server.Application.UnitTests.Common;

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
    [InlineData(typeof(Church), typeof(ChurchDto))]
    [InlineData(typeof(ChurchCreateDto), typeof(Church))]
    [InlineData(typeof(Enrollment), typeof(StudentEnrollmentDto))]
    [InlineData(typeof(Enrollment), typeof(GenerationEnrollmentDto))]
    [InlineData(typeof(Student), typeof(StudentDto))]
    [InlineData(typeof(Generation), typeof(GenerationDto))]
    [InlineData(typeof(Grade), typeof(GradeDto))]
    [InlineData(typeof(StudentCreateDto), typeof(Student))]
    [InlineData(typeof(Teacher), typeof(TeacherDto))]
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