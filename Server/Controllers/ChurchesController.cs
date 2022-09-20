using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gbs.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ChurchesController : ControllerBase
{
    private readonly IChurchRepository _churchRepo;

    public ChurchesController(IChurchRepository churchRepo)
    {
        _churchRepo = churchRepo;
    }

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<Church>>>> GetChurches()
    {
        var response = await _churchRepo.GetAllChurches();
        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpGet("{churchId:int}")]
    public async Task<ActionResult<ServiceResponse<Church>>> GetChurch(int churchId)
    {
        var response = await _churchRepo.GetChurchById(churchId);
        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Roles = $"{Roles.Admin}, {Roles.SuperAdmin}, {Roles.Teacher}")]
    public async Task<ActionResult<ServiceResponse<List<Church>>>> AddChurch(ChurchCreateDto church)
    {
        var response = await _churchRepo.AddChurch(church);
        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpPut("{churchId:int}")]
    [Authorize(Roles = $"{Roles.Admin}, {Roles.SuperAdmin}, {Roles.Teacher}")]
    public async Task<ActionResult<ServiceResponse<Church>>> UpdateChurch(int churchId, ChurchCreateDto church)
    {
        var response = await _churchRepo.UpdateChurch(churchId, church);
        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpDelete("{churchId:int}")]
    [Authorize(Roles = $"{Roles.Admin}, {Roles.SuperAdmin}, {Roles.Teacher}")]
    public async Task<ActionResult<ServiceResponse<List<Church>>>> DeleteChurch(int churchId)
    {
        var response = await _churchRepo.DeleteChurch(churchId);
        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }
}