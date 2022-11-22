namespace Gbs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = Policies.RequireAdmins)]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly IIdentityQueries _identityQueries;
        private readonly IIdentityCommands _identityCommands;

        public UsersController(IUserRepository userRepo, IIdentityQueries identityQueries, IIdentityCommands identityCommands)
        {
            _userRepo = userRepo;
            _identityQueries = identityQueries;
            _identityCommands = identityCommands;
        }

        [HttpGet]
        public async Task<ActionResult<Result<List<UserDto>>>> GetUsers()
        {
            var result = await _identityQueries.GetAll();
            return result.ToActionResult();
        }

        [HttpPut("{id}/church")]
        public async Task<ActionResult<Result<UserDto>>> UpdateChurch(string id, UserUpdateChurchDto updateDto)
        {
            var result = await _identityCommands.UpdateChurch(id, updateDto);
            return result.ToActionResult();
        }

        [HttpPut("{id}/roles")]
        public async Task<ActionResult<Result<UserDto>>> UpdateRoles(string id,
            [FromBody] UserUpdateRoleDto updateRoleDto)
        {
            var user = await _identityCommands.UpdateRoles(id, updateRoleDto.Roles);
            return user.ToActionResult();
        }

        [HttpPut("{id}/active")]
        public async Task<ActionResult<Result<UserDto>>> UpdateUserActiveState(string id,
            UserUpdateActiveStateDto updateActiveStateDto)
        {
            var user = await _identityCommands.UpdateActiveState(id, updateActiveStateDto.IsActive);
            return user.ToActionResult();
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<Result<bool>>> DeleteUser(string id)
        {
            var user = await _identityCommands.Delete(id);
            return user.ToActionResult();
        }
    }
}