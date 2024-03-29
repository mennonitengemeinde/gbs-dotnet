using Gbs.Application.Features.Generations.Interfaces;

namespace Gbs.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class GenerationsController : ControllerBase
{
    private readonly IGenerationCommands _generationCommands;
    private readonly IGenerationQueries _generationQueries;

    public GenerationsController(IGenerationQueries generationQueries, IGenerationCommands generationCommands)
    {
        _generationQueries = generationQueries;
        _generationCommands = generationCommands;
    }

    [HttpGet]
    [Authorize(Policy = Policies.RequireAdminsSoundAndTeachers)]
    public async Task<ActionResult<Result<List<GenerationResponse>>>> GetGenerations()
    {
        var result = await _generationQueries.GetAll();
        return result.ToActionResult();
    }

    [HttpPost]
    [Authorize(Policy = Policies.RequireAdmins)]
    public async Task<ActionResult<Result<GenerationResponse>>> AddGeneration(CreateGenerationRequest request)
    {
        var result = await _generationCommands.Add(request);
        return result.ToActionResult();
    }

    [HttpGet("{id:int}")]
    [Authorize(Policy = Policies.RequireAdmins)]
    public async Task<ActionResult<Result<GenerationResponse>>> GetGenerationById(int id)
    {
        var result = await _generationQueries.GetById(id);
        return result.ToActionResult();
    }

    [HttpPut("{generationId:int}")]
    [Authorize(Policy = Policies.RequireAdmins)]
    public async Task<ActionResult<Result<GenerationResponse>>> UpdateGeneration(int generationId,
        CreateGenerationRequest generation)
    {
        var result = await _generationCommands.Update(generationId, generation);
        return result.ToActionResult();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Policy = Policies.RequireAdmins)]
    public async Task<ActionResult<Result<bool>>> DeleteGeneration(int id)
    {
        var result = await _generationCommands.Delete(id);
        return result.ToActionResult();
    }
}