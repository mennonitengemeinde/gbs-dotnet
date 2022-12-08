using AutoMapper;
using Gbs.Application.Features.Churches;
using Gbs.Application.Features.Generations;
using Gbs.Application.Features.Grades;
using Gbs.Application.Features.Identity;
using Gbs.Application.Features.Lessons;
using Gbs.Application.Features.Streams;
using Gbs.Application.Features.Students;
using Gbs.Application.Features.Subjects;
using Gbs.Application.Features.Teachers;
using Gbs.Infrastructure.Persistence;
using Gbs.Tests.Infrastructure.Seeds;
using Microsoft.EntityFrameworkCore;

namespace Gbs.Tests.Infrastructure;

public class GbsTestBase : IDisposable
{
    protected GbsTestBase()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        Context = new DataContext(options);
        Context.Database.EnsureCreated();

        var churchList = ChurchSeed.GetChurches();
        Context.Churches.AddRange(churchList);
        
        var generationList = GenerationSeed.GetGenerations();
        Context.Generations.AddRange(generationList);
        
        var lessonList = LessonSeed.GetLessons();
        Context.Lessons.AddRange(lessonList);
        
        var streamList = StreamSeed.GetStreams();
        Context.Streams.AddRange(streamList);
        
        var subjectList = SubjectSeed.GetSubjects();
        Context.Subjects.AddRange(subjectList);
        
        var teacherList = TeacherSeed.GetTeachers();
        Context.Teachers.AddRange(teacherList);
        
        Context.SaveChanges();
        
        Mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ChurchMapping());
            cfg.AddProfile(new GenerationMapping());
            cfg.AddProfile<GradeMapping>();
            cfg.AddProfile<IdentityMapping>();
            cfg.AddProfile<LessonMapping>();
            cfg.AddProfile(new StreamMapping());
            cfg.AddProfile<StudentMapping>();
            cfg.AddProfile(new SubjectMapping());
            cfg.AddProfile(new TeacherMapping());
        }).CreateMapper();
    }

    protected DataContext Context { get; private set; }
    protected IMapper Mapper { get; }

    public void Dispose()
    {
        Context.Database.EnsureDeleted();
        Context.Dispose();
    }
}