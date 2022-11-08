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
    private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;
    
    public MappingTests()
    {
        _configuration = new MapperConfiguration(config => 
            config.AddProfile<MappingProfile>());
        
        _mapper = _configuration.CreateMapper();
    }
    
    [Test]
    public void ShouldHaveValidConfiguration()
    {
        _configuration.AssertConfigurationIsValid();
    }
    
    [Test]
    [TestCase(typeof(Church), typeof(ChurchDto))]
    [TestCase(typeof(ChurchCreateDto), typeof(Church))]
    [TestCase(typeof(Enrollment), typeof(StudentEnrollmentDto))]
    [TestCase(typeof(Enrollment), typeof(GenerationEnrollmentDto))]
    [TestCase(typeof(Student), typeof(StudentDto))]
    [TestCase(typeof(Generation), typeof(GenerationDto))]
    [TestCase(typeof(Grade), typeof(GradeDto))]
    [TestCase(typeof(Student), typeof(StudentDto))]
    [TestCase(typeof(StudentCreateDto), typeof(Student))]
    [TestCase(typeof(Teacher), typeof(TeacherDto))]
    public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
    {
        var instance = GetInstanceOf(source);

        _mapper.Map(instance, source, destination);
    }
    
    private object GetInstanceOf(Type type)
    {
        if (type.GetConstructor(Type.EmptyTypes) != null)
            return Activator.CreateInstance(type)!;

        // Type without parameterless constructor
        return FormatterServices.GetUninitializedObject(type);
    }
}