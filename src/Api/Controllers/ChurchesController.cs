using Gbs.Application.Features.Churches.Interfaces;
using Gbs.Shared.Churches;

namespace Gbs.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Policy = Policies.RequireAdmins)]
public class ChurchesController : ControllerBase
{
    private readonly IChurchQueries _churchQueries;
    private readonly IChurchCommands _churchCommands;

    public ChurchesController(IChurchQueries churchQueries, IChurchCommands churchCommands)
    {
        _churchQueries = churchQueries;
        _churchCommands = churchCommands;
    }
    
    [HttpGet]
    public async Task<ActionResult<Result<List<ChurchResponse>>>> GetChurches()
    {
        var result = await _churchQueries.GetAll();
        return result.ToActionResult();
    }

    [HttpGet("{churchId:int}")]
    public async Task<ActionResult<Result<ChurchResponse>>> GetChurch(int churchId)
    {
        var result = await _churchQueries.GetById(churchId);
        return result.ToActionResult();
    }

    [HttpPost]
    public async Task<ActionResult<Result<ChurchResponse>>> AddChurch(CreateChurchRequest request)
    {
        var result = await _churchCommands.Add(request);
        // var result = await _churchCommands.Add(church);
        return result.ToActionResult();
    }

    [HttpPut("{churchId:int}")]
    public async Task<ActionResult<Result<ChurchResponse>>> UpdateChurch(int churchId, UpdateChurchRequest church)
    {
        var result = await _churchCommands.Update(church);
        return result.ToActionResult();
    }

    [HttpDelete("{churchId:int}")]
    public async Task<ActionResult<Result<bool>>> DeleteChurch(int churchId)
    {
        var result = await _churchCommands.Delete(churchId);
        return result.ToActionResult();
    }
}