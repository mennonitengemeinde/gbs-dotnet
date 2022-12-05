using Gbs.Application.Churches.Commands;
using Gbs.Application.Churches.Queries;
using Gbs.Shared.Churches;

namespace Gbs.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Policy = Policies.RequireAdmins)]
public class ChurchesController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<Result<List<ChurchDto>>>> GetChurches()
    {
        var result = await Mediator.Send(new GetChurchesQuery());
        return result.ToActionResult();
    }

    [HttpGet("{churchId:int}")]
    public async Task<ActionResult<Result<ChurchDto>>> GetChurch(int churchId)
    {
        var result = await Mediator.Send(new GetChurchQuery(churchId));
        return result.ToActionResult();
    }

    [HttpPost]
    public async Task<ActionResult<Result<ChurchDto>>> AddChurch(CreateChurchRequest request)
    {
        var result = await Mediator.Send(new CreateChurchCommand(request));
        // var result = await _churchCommands.Add(church);
        return result.ToActionResult();
    }

    [HttpPut("{churchId:int}")]
    public async Task<ActionResult<Result<ChurchDto>>> UpdateChurch(int churchId, CreateChurchRequest church)
    {
        var result = await Mediator.Send(new UpdateChurchCommand(churchId, church));
        return result.ToActionResult();
    }

    [HttpDelete("{churchId:int}")]
    public async Task<ActionResult<Result<bool>>> DeleteChurch(int churchId)
    {
        var result = await Mediator.Send(new DeleteChurchCommand(churchId));
        return result.ToActionResult();
    }
}