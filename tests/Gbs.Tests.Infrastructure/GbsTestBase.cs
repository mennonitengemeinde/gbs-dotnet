using Gbs.Infrastructure.Persistence;
using Gbs.Infrastructure.Persistence.Seeds;
using Microsoft.EntityFrameworkCore;

namespace Gbs.Tests.Infrastructure;

public class GbsTestBase : IDisposable
{
    public GbsTestBase()
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
        var subjectList = SubjectSeed.GetSubjects();
        Context.Subjects.AddRange(subjectList);
        var teacherList = TeacherSeed.GetTeachers();
        Context.Teachers.AddRange(teacherList);
        Context.SaveChanges();
    }

    public DataContext Context { get; private set; }

    public void Dispose()
    {
        Context.Database.EnsureDeleted();
        Context.Dispose();
    }
}