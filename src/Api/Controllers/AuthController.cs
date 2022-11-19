namespace Gbs.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthRepository _authRepo;
    private readonly IUserCommands _userCommands;

    public AuthController(IAuthRepository authRepo, IUserCommands userCommands)
    {
        _authRepo = authRepo;
        _userCommands = userCommands;
    }

    [HttpPost("register")]
    public async Task<ActionResult<Result<string>>> RegisterUser(RegisterDto request)
    {
        var result = await _userCommands.Add(request);
        return result.ToActionResult();
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<Result<int>>> Login(LoginDto request)
    {
        var result = await _authRepo.Login(request.Email, request.Password);
        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
    
    [HttpGet("roles")]
    public async Task<ActionResult<Result<List<RolesDto>>>> GetRoles()
    {
        var result = await _authRepo.GetRoles();
        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}