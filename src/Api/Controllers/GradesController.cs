using Gbs.Application.Features.Grades.Interfaces;
using Gbs.Shared.Grades;

namespace Gbs.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GradesController : ControllerBase
{
    private readonly IGradeCommands _gradeCommands;

    public GradesController(IGradeCommands gradeCommands)
    {
        _gradeCommands = gradeCommands;
    }

    [HttpPost]
    [Authorize(Policy = Policies.RequireAdmins)]
    public async Task<ActionResult<Result<GradeResponse>>> AddGrade(CreateGradeRequest request)
    {
        var result = await _gradeCommands.Add(request);
        return result.ToActionResult();
    }

    [HttpPut("{id:int}")]
    [Authorize(Policy = Policies.RequireAdmins)]
    public async Task<ActionResult<Result<GradeResponse>>> UpdateGrade(int id, CreateGradeRequest request)
    {
        var result = await _gradeCommands.Update(id, request);
        return result.ToActionResult();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Policy = Policies.RequireAdmins)]
    public async Task<ActionResult<Result<bool>>> DeleteGrade(int id)
    {
        var result = await _gradeCommands.Delete(id);
        return result.ToActionResult();
    }
}