namespace Gbs.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IIdentityQueries _identityQueries;
    private readonly IIdentityCommands _identityCommands;

    public AuthController(IIdentityQueries identityQueries, IIdentityCommands identityCommands)
    {
        _identityQueries = identityQueries;
        _identityCommands = identityCommands;
    }

    [HttpPost("login")]
    public async Task<ActionResult<Result<string>>> Login(LoginDto request)
    {
        var result = await _identityCommands.Login(request.Email, request.Password);
        return result.ToActionResult();
    }

    [HttpGet("roles")]
    public async Task<ActionResult<Result<List<RolesDto>>>> GetRoles()
    {
        var result = await _identityQueries.GetRoles();
        return result.ToActionResult();
    }
}