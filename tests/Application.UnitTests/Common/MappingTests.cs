﻿using System.Runtime.Serialization;
using AutoMapper;
using Gbs.Shared.Dto.Streams;
using Gbs.Shared.Dto.Subjects;

namespace Gbs.Application.UnitTests.Common;

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
    [InlineData(typeof(Lesson), typeof(SubjectLessonDto))]
    [InlineData(typeof(LiveStream), typeof(StreamDto))]
    [InlineData(typeof(LiveStreamTeacher), typeof(TeacherDto))]
    [InlineData(typeof(StreamCreateDto), typeof(LiveStream))]
    [InlineData(typeof(StudentCreateDto), typeof(Student))]
    [InlineData(typeof(Subject), typeof(SubjectDto))]
    [InlineData(typeof(SubjectCreateDto), typeof(Subject))]
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