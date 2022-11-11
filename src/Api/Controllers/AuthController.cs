namespace Gbs.Api.Controllers;

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
    public async Task<ActionResult<Result<string>>> RegisterUser(RegisterDto request)
    {
        var result = await _authRepo.Register(request);
        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);
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