using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace gbs.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthRepository _authRepo;

    public AuthController(IAuthRepository authRepo)
    {
        _authRepo = authRepo;
    }

    [HttpPost("register")]
    public async Task<ActionResult<ServiceResponse<string>>> RegisterUser(RegisterDto request)
    {
        var result = await _authRepo.Register(request);
        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<ServiceResponse<int>>> Login(LoginDto request)
    {
        var result = await _authRepo.Login(request.Email, request.Password);
        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
    
    [HttpGet("roles")]
    public async Task<ActionResult<ServiceResponse<List<RolesResponse>>>> GetRoles()
    {
        var result = await _authRepo.GetRoles();
        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}