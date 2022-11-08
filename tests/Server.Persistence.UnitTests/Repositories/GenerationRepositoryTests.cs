using Gbs.Server.Persistence.Seeds;
using System.Linq;

namespace gbs.Server.Persistence.UnitTests.Repositories;

public class GenerationRepositoryTests
{
    private DataContext _context = null!;
    private GenerationRepository _repository = null!;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "GenerationDatabase")
            .Options;

        _context = new DataContext(options);
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();

        _context.Subjects.AddRange(SubjectSeed.GetSubjects());
        _context.Generations.AddRange(GenerationSeed.GetGenerations());
        _context.Enrollments.AddRange(EnrollmentSeed.GetEnrollments());
        _context.Students.AddRange(StudentSeed.GetStudents());
        _context.Grades.AddRange(GradeSeed.GetGrades());
        _context.SaveChanges();
        
        var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
        var mapper = config.CreateMapper();
        
        _repository = new GenerationRepository(_context, mapper);
    }
    
    [Test]
    public async Task GetGenerationsAsync_ReturnsAllGenerations()
    {
        var generations = await _repository.GetAllGenerations();
        
        Assert.That(generations.Data, Has.Count.EqualTo(3));
    }
    
    [Test]
    public async Task GetGenerationById_ReturnsGeneration()
    {
        var generation = await _repository.GetGenerationById(1);
        
        Assert.Multiple(() =>
        {
            Assert.That(generation.Data, Is.Not.Null);
            Assert.That(generation.Data.Name, Is.EqualTo("Generation 1"));
            Assert.That(generation.Data.Enrollments, Has.Count.EqualTo(3));
        });
    }
}