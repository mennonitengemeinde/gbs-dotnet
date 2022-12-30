using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gbs.Application.Features.GradeTypes.Interfaces;
using Gbs.Shared.GradeTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gbs.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GradeTypesController : ControllerBase
{
    private readonly IGradeTypeQueries _gradeTypeQueries;
    private readonly IGradeTypeCommands _gradeTypeCommands;

    public GradeTypesController(IGradeTypeQueries gradeTypeQueries, IGradeTypeCommands gradeTypeCommands)
    {
        _gradeTypeQueries = gradeTypeQueries;
        _gradeTypeCommands = gradeTypeCommands;
    }
    
    [HttpGet]
    [Authorize (Policy = Policies.RequireAdminsSoundAndTeachers)]
    public async Task<ActionResult<Result<List<GradeTypeResponse>>>> GetGradeTypes()
    {
        var result = await _gradeTypeQueries.GetAll();
        return result.ToActionResult();
    }
    
    [HttpPost]
    [Authorize(Policy = Policies.RequireAdmins)]
    public async Task<ActionResult<Result<GradeTypeResponse>>> AddGradeType(CreateGradeTypeRequest request)
    {
        var result = await _gradeTypeCommands.Add(request);
        return result.ToActionResult();
    }

    [HttpGet("{id:int}")]
    [Authorize(Policy = Policies.RequireAdminsSoundAndTeachers)]
    public async Task<ActionResult<Result<GradeTypeResponse>>> GetGradeTypeById(int id)
    {
        var result = await _gradeTypeQueries.GetById(id);
        return result.ToActionResult();
    }

    [HttpPut("{id:int}")]
    [Authorize(Policy = Policies.RequireAdmins)]
    public async Task<ActionResult<Result<GradeTypeResponse>>> UpdateGradeType(int id,
        CreateGradeTypeRequest request)
    {
        var result = await _gradeTypeCommands.Update(id, request);
        return result.ToActionResult();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Policy = Policies.RequireAdmins)]
    public async Task<ActionResult<Result<bool>>> DeleteGeneration(int id)
    {
        var result = await _gradeTypeCommands.Delete(id);
        return result.ToActionResult();
    }
    
}