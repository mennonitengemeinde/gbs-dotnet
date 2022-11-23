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
    public async Task<ActionResult<Result<List<ChurchDto>>>> GetChurches()
    {
        var result = await _churchQueries.GetAll();
        return result.ToActionResult();
    }

    [HttpGet("{churchId:int}")]
    public async Task<ActionResult<Result<ChurchDto>>> GetChurch(int churchId)
    {
        var result = await _churchQueries.GetById(churchId);
        return result.ToActionResult();
    }

    [HttpPost]
    public async Task<ActionResult<Result<ChurchDto>>> AddChurch(ChurchCreateDto church)
    {
        var result = await _churchCommands.Add(church);
        return result.ToActionResult();
    }

    [HttpPut("{churchId:int}")]
    public async Task<ActionResult<Result<ChurchDto>>> UpdateChurch(int churchId, ChurchCreateDto church)
    {
        var result = await _churchCommands.Update(churchId, church);
        return result.ToActionResult();
    }

    [HttpDelete("{churchId:int}")]
    public async Task<ActionResult<Result<bool>>> DeleteChurch(int churchId)
    {
        var result = await _churchCommands.Delete(churchId);
        return result.ToActionResult();
    }
}