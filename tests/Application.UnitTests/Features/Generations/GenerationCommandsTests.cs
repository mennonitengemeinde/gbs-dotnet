using FluentValidation;
using Gbs.Application.Features.Generations;
using Gbs.Application.Features.Generations.Validators;
using Gbs.Shared.Generations;

namespace Gbs.Tests.Application.UnitTests.Features.Generations;

public class GenerationCommandsTests : GbsTestBase
{
    IMapper Mapper { get; }
    CreateGenerationValidator CreateGenerationRequestValidator { get; }
    UpdateGenerationValidator UpdateGenerationRequestValidator { get; }

    public GenerationCommandsTests()
    {
        var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
        Mapper = config.CreateMapper();
        CreateGenerationRequestValidator = new CreateGenerationValidator(Context);
        UpdateGenerationRequestValidator = new UpdateGenerationValidator(Context);
    }
    
    [Fact]
    public async Task Add_AddsGeneration()
    {
        
        var q = new GenerationQueries(Context, Mapper);
        var cmd = new GenerationCommands(Context, q, CreateGenerationRequestValidator, UpdateGenerationRequestValidator);
        var newGen = new CreateGenerationRequest { Name = "Generation 4" };
        
        var result = await cmd.Add(newGen);
        var ctxCount = Context.Generations.Count();
        
        Assert.True(result.Success);
        Assert.Equal(200, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(newGen.Name, result.Data.Name);
        Assert.Equal(4, ctxCount);
    }
    
    [Fact]
    public async Task Add_ReturnsBadRequest_WhenNameAlreadyExists()
    {
        var q = new GenerationQueries(Context, Mapper);
        var cmd = new GenerationCommands(Context, q, CreateGenerationRequestValidator, UpdateGenerationRequestValidator);
        var newGen = new CreateGenerationRequest { Name = "Generation 1" };
        
        var result = await cmd.Add(newGen);
        
        Assert.False(result.Success);
        Assert.Equal(422, result.StatusCode);
        Assert.Null(result.Data);
    }
    
    [Fact]
    public async Task Update_UpdatesGeneration()
    {
        var q = new GenerationQueries(Context, Mapper);
        var cmd = new GenerationCommands(Context, q, CreateGenerationRequestValidator, UpdateGenerationRequestValidator);
        var updateGen = new UpdateGenerationRequest { Id = 1, Name = "Generation 1 Updated" };
        
        var result = await cmd.Update(1, updateGen);
        
        Assert.True(result.Success);
        Assert.Equal(200, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(updateGen.Name, result.Data.Name);
    }
    
    [Fact]
    public async Task Update_ReturnsNotFound_WhenIdDoesNotExist()
    {
        var q = new GenerationQueries(Context, Mapper);
        var cmd = new GenerationCommands(Context, q, CreateGenerationRequestValidator, UpdateGenerationRequestValidator);
        var updateGen = new UpdateGenerationRequest { Id = 1, Name = "Generation 1 Updated" };
        
        var result = await cmd.Update(999, updateGen);
        
        Assert.False(result.Success);
        Assert.Equal(404, result.StatusCode);
        Assert.Null(result.Data);
    }
    
    [Fact]
    public async Task Update_ReturnsBadRequest_WhenNameAlreadyExists()
    {
        var q = new GenerationQueries(Context, Mapper);
        var cmd = new GenerationCommands(Context, q, CreateGenerationRequestValidator, UpdateGenerationRequestValidator);
        var updateGen = new UpdateGenerationRequest { Id = 1, Name = "Generation 2" };
        
        var result = await cmd.Update(1, updateGen);
        
        Assert.False(result.Success);
        Assert.Equal(422, result.StatusCode);
        Assert.Null(result.Data);
    }
    
    [Fact]
    public async Task Delete_DeletesGeneration()
    {
        var q = new GenerationQueries(Context, Mapper);
        var cmd = new GenerationCommands(Context, q, CreateGenerationRequestValidator, UpdateGenerationRequestValidator);
        
        var result = await cmd.Delete(1);
        var ctxCount = Context.Generations.Count();
        
        Assert.True(result.Success);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal(2, ctxCount);
    }
    
    [Fact]
    public async Task Delete_ReturnsNotFound_WhenIdDoesNotExist()
    {
        var q = new GenerationQueries(Context, Mapper);
        var cmd = new GenerationCommands(Context, q, CreateGenerationRequestValidator, UpdateGenerationRequestValidator);
        
        var result = await cmd.Delete(999);
        
        Assert.False(result.Success);
        Assert.Equal(404, result.StatusCode);
    }
}