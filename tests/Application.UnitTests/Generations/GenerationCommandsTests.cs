using Gbs.Application.Generations;

namespace Gbs.Tests.Application.UnitTests.Generations;

public class GenerationCommandsTests : GbsTestBase
{
    [Fact]
    public async Task Add_AddsGeneration()
    {
        var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
        var mapper = config.CreateMapper();
        var q = new GenerationQueries(Context, mapper);
        var cmd = new GenerationCommands(Context, q);
        var newGen = new GenerationCreateDto { Name = "Generation 4" };
        
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
        var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
        var mapper = config.CreateMapper();
        var q = new GenerationQueries(Context, mapper);
        var cmd = new GenerationCommands(Context, q);
        var newGen = new GenerationCreateDto { Name = "Generation 1" };
        
        var result = await cmd.Add(newGen);
        
        Assert.False(result.Success);
        Assert.Equal(400, result.StatusCode);
        Assert.Null(result.Data);
    }
    
    [Fact]
    public async Task Update_UpdatesGeneration()
    {
        var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
        var mapper = config.CreateMapper();
        var q = new GenerationQueries(Context, mapper);
        var cmd = new GenerationCommands(Context, q);
        var updateGen = new GenerationUpdateDto { Name = "Generation 1 Updated" };
        
        var result = await cmd.Update(1, updateGen);
        
        Assert.True(result.Success);
        Assert.Equal(200, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(updateGen.Name, result.Data.Name);
    }
    
    [Fact]
    public async Task Update_ReturnsNotFound_WhenIdDoesNotExist()
    {
        var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
        var mapper = config.CreateMapper();
        var q = new GenerationQueries(Context, mapper);
        var cmd = new GenerationCommands(Context, q);
        var updateGen = new GenerationUpdateDto { Name = "Generation 1 Updated" };
        
        var result = await cmd.Update(999, updateGen);
        
        Assert.False(result.Success);
        Assert.Equal(404, result.StatusCode);
        Assert.Null(result.Data);
    }
    
    [Fact]
    public async Task Update_ReturnsBadRequest_WhenNameAlreadyExists()
    {
        var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
        var mapper = config.CreateMapper();
        var q = new GenerationQueries(Context, mapper);
        var cmd = new GenerationCommands(Context, q);
        var updateGen = new GenerationUpdateDto { Name = "Generation 2" };
        
        var result = await cmd.Update(1, updateGen);
        
        Assert.False(result.Success);
        Assert.Equal(400, result.StatusCode);
        Assert.Null(result.Data);
    }
    
    [Fact]
    public async Task Delete_DeletesGeneration()
    {
        var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
        var mapper = config.CreateMapper();
        var q = new GenerationQueries(Context, mapper);
        var cmd = new GenerationCommands(Context, q);
        
        var result = await cmd.Delete(1);
        var ctxCount = Context.Generations.Count();
        
        Assert.True(result.Success);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal(2, ctxCount);
    }
    
    [Fact]
    public async Task Delete_ReturnsNotFound_WhenIdDoesNotExist()
    {
        var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
        var mapper = config.CreateMapper();
        var q = new GenerationQueries(Context, mapper);
        var cmd = new GenerationCommands(Context, q);
        
        var result = await cmd.Delete(999);
        
        Assert.False(result.Success);
        Assert.Equal(404, result.StatusCode);
    }
}