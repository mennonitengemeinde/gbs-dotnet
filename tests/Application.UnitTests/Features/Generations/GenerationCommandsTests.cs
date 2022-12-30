using Gbs.Application.Features.Generations;
using Gbs.Shared.Generations;

namespace Gbs.Tests.Application.UnitTests.Features.Generations;

public class GenerationCommandsTests : GenerationTestBase, ICommandTests
{
    [Fact]
    public async Task Add_AddsNewEntity()
    {
        var q = new GenerationQueries(Context, Mapper);
        var cmd = new GenerationCommands(Context, q, Validator);
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
    public async Task Add_ReturnValidationError_WhenEntityAlreadyExists()
    {
        var q = new GenerationQueries(Context, Mapper);
        var cmd = new GenerationCommands(Context, q, Validator);
        var newGen = new CreateGenerationRequest { Name = "Generation 1" };

        var result = await cmd.Add(newGen);

        Assert.False(result.Success);
        Assert.Equal(422, result.StatusCode);
        Assert.NotNull(result.Errors);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task Update_UpdatesEntity()
    {
        var q = new GenerationQueries(Context, Mapper);
        var cmd = new GenerationCommands(Context, q, Validator);
        var updateGen = new CreateGenerationRequest { Name = "Generation 1 Updated" };

        var result = await cmd.Update(1, updateGen);

        Assert.True(result.Success);
        Assert.Equal(200, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(updateGen.Name, result.Data.Name);
    }

    [Fact]
    public async Task Update_ReturnsNotFound_WhenEntityDoesNotExist()
    {
        var q = new GenerationQueries(Context, Mapper);
        var cmd = new GenerationCommands(Context, q, Validator);
        var updateGen = new CreateGenerationRequest { Name = "Generation 1 Updated" };

        var result = await cmd.Update(999, updateGen);

        Assert.False(result.Success);
        Assert.Equal(404, result.StatusCode);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task Update_ReturnValidationError_WhenEntityAlreadyExists()
    {
        var q = new GenerationQueries(Context, Mapper);
        var cmd = new GenerationCommands(Context, q, Validator);
        var updateGen = new CreateGenerationRequest { Name = "Generation 2" };

        var result = await cmd.Update(1, updateGen);

        Assert.False(result.Success);
        Assert.Equal(422, result.StatusCode);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task Delete_DeletesEntity()
    {
        var q = new GenerationQueries(Context, Mapper);
        var cmd = new GenerationCommands(Context, q, Validator);

        var result = await cmd.Delete(1);
        var ctxCount = Context.Generations.Count();

        Assert.True(result.Success);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal(2, ctxCount);
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_WhenEntityDoesNotExist()
    {
        var q = new GenerationQueries(Context, Mapper);
        var cmd = new GenerationCommands(Context, q, Validator);

        var result = await cmd.Delete(999);

        Assert.False(result.Success);
        Assert.Equal(404, result.StatusCode);
    }
}