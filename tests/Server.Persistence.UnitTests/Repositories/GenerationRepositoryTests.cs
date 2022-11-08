using Gbs.Core.Domain.Dto.Generations;

namespace gbs.Server.Persistence.UnitTests.Repositories;

public class GenerationRepositoryTests
{
    private readonly GenerationRepository _generationRepo;

    public GenerationRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "GenerationDatabase")
            .Options;
    
        var context = new DataContext(options);
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    
        context.Subjects.AddRange(SubjectSeed.GetSubjects());
        context.Generations.AddRange(GenerationSeed.GetGenerations());
        context.Enrollments.AddRange(EnrollmentSeed.GetEnrollments());
        context.Students.AddRange(StudentSeed.GetStudents());
        context.Grades.AddRange(GradeSeed.GetGrades());
        context.SaveChanges();
        
        var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
        var mapper = config.CreateMapper();
        
        _generationRepo = new GenerationRepository(context, mapper);
    }
    
    [Fact]
    public async Task GetGenerationsAsync_ReturnsAllGenerations()
    {
        var generations = await _generationRepo.GetAllGenerations();
        
        Assert.Equal(3, generations.Data.Count);
    }
    
    [Fact]
    public async Task GetGenerationById_ReturnsGeneration()
    {
        var generation = await _generationRepo.GetGenerationById(1);
        
        Assert.Equal("Generation 1", generation.Data.Name);
        Assert.Equal(1, generation.Data.Id);
        Assert.Equal(3, generation.Data.Enrollments.Count);
    }
    
    [Fact]
    public async Task GetGenerationById_ReturnsNotFound()
    {
        var generation = await _generationRepo.GetGenerationById(4);
        
        Assert.Equal(404, generation.StatusCode);
    }
    
    [Fact]
    public async Task AddGeneration_ReturnsCreatedGeneration()
    {
        var dto = new GenerationCreateDto()
        {
            Name = "Generation 4",
        };
        
        var generation = await _generationRepo.AddGeneration(dto);
        
        Assert.Equal("Generation 4", generation.Data.Name);
        Assert.Equal(4, generation.Data.Id);
        Assert.Empty(generation.Data.Enrollments);
    }
    
    [Fact]
    public async Task AddGeneration_ReturnsBadRequest()
    {
        var dto = new GenerationCreateDto()
        {
            Name = "Generation 1",
        };
        
        var generation = await _generationRepo.AddGeneration(dto);
        
        Assert.Equal(400, generation.StatusCode);
    }
    
    [Fact]
    public async Task UpdateGeneration_ReturnsUpdatedGeneration()
    {
        var dto = new GenerationUpdateDto()
        {
            Name = "Generation 1 Updated",
        };
        
        var generation = await _generationRepo.UpdateGeneration(1, dto);
        
        Assert.Equal("Generation 1 Updated", generation.Data.Name);
        Assert.Equal(1, generation.Data.Id);
        Assert.Equal(3, generation.Data.Enrollments.Count);
    }
    
    [Fact]
    public async Task UpdateGeneration_ReturnsNotFound()
    {
        var dto = new GenerationUpdateDto()
        {
            Name = "Generation 1 Updated",
        };
        
        var generation = await _generationRepo.UpdateGeneration(4, dto);
        
        Assert.Equal(404, generation.StatusCode);
    }
    
    [Fact]
    public async Task UpdateGeneration_ReturnsBadRequest()
    {
        var dto = new GenerationUpdateDto()
        {
            Name = "Generation 1",
        };
        
        var generation = await _generationRepo.UpdateGeneration(2, dto);
        
        Assert.Equal(400, generation.StatusCode);
    }
    
    [Fact]
    public async Task DeleteGeneration_ReturnsOk()
    {
        var generation = await _generationRepo.DeleteGeneration(1);
        
        Assert.Equal(200, generation.StatusCode);
    }
    
    [Fact]
    public async Task DeleteGeneration_ReturnsNotFound()
    {
        var generation = await _generationRepo.DeleteGeneration(4);
        
        Assert.Equal(404, generation.StatusCode);
    }
}