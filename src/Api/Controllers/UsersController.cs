namespace Gbs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = Policies.RequireAdmins)]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly IUserQueries _userQueries;
        private readonly IUserCommands _userCommands;

        public UsersController(IUserRepository userRepo, IUserQueries userQueries, IUserCommands userCommands)
        {
            _userRepo = userRepo;
            _userQueries = userQueries;
            _userCommands = userCommands;
        }

        [HttpGet]
        public async Task<ActionResult<Result<List<UserDto>>>> GetUsers()
        {
            var result = await _userQueries.GetAll();
            return result.ToActionResult();
        }

        [HttpPut("{id}/church")]
        public async Task<ActionResult<Result<UserDto>>> UpdateChurch(string id, UserUpdateChurchDto updateDto)
        {
            var result = await _userCommands.UpdateChurch(id, updateDto);
            return result.ToActionResult();
        }

        [HttpPut("{id}/roles")]
        public async Task<ActionResult<Result<UserDto>>> UpdateRoles(string id,
            [FromBody] UserUpdateRoleDto updateRoleDto)
        {
            var user = await _userCommands.UpdateRoles(id, updateRoleDto.Roles);
            return user.ToActionResult();
        }

        [HttpPut("{id}/active")]
        public async Task<ActionResult<Result<UserDto>>> UpdateUserActiveState(string id,
            UserUpdateActiveStateDto updateActiveStateDto)
        {
            var user = await _userCommands.UpdateActiveState(id, updateActiveStateDto.IsActive);
            return user.ToActionResult();
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<Result<bool>>> DeleteUser(string id)
        {
            var user = await _userCommands.Delete(id);
            return user.ToActionResult();
        }
    }
}