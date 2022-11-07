using Gbs.Core.Domain.Dto.Churches;
using Gbs.Server.Application.Common.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gbs.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Policy = Policies.RequireAdmins)]
public class ChurchesController : ControllerBase
{
    private readonly IChurchRepository _churchRepo;

    public ChurchesController(IChurchRepository churchRepo)
    {
        _churchRepo = churchRepo;
    }

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<ChurchDto>>>> GetChurches()
    {
        var response = await _churchRepo.GetAllChurches();
        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpGet("{churchId:int}")]
    public async Task<ActionResult<ServiceResponse<ChurchDto>>> GetChurch(int churchId)
    {
        var response = await _churchRepo.GetChurchById(churchId);
        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<List<ChurchDto>>>> AddChurch(ChurchCreateDto church)
    {
        var response = await _churchRepo.AddChurch(church);
        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpPut("{churchId:int}")]
    public async Task<ActionResult<ServiceResponse<ChurchDto>>> UpdateChurch(int churchId, ChurchCreateDto church)
    {
        var response = await _churchRepo.UpdateChurch(churchId, church);
        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpDelete("{churchId:int}")]
    public async Task<ActionResult<ServiceResponse<List<ChurchDto>>>> DeleteChurch(int churchId)
    {
        var response = await _churchRepo.DeleteChurch(churchId);
        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }
}