namespace Gbs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = Policies.RequireAdmins)]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepo;

        public UsersController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet]
        public async Task<ActionResult<Result<List<UserDto>>>> GetUsers()
        {
            var users = await _userRepo.GetUsers();
            return Ok(users);
        }

        [HttpPut("{userId}/church")]
        public async Task<ActionResult<Result<List<UserDto>>>> UpdateUser(string userId, UserUpdateChurchDto updateDto)
        {
            var user = await _userRepo.UpdateUserChurch(userId, updateDto);
            if (!user.Success)
            {
                return BadRequest(user);
            }

            return Ok(user);
        }

        [HttpPut("{userId}/roles")]
        public async Task<ActionResult<Result<List<UserDto>>>> UpdateUserRole(string userId,
            [FromBody] UserUpdateRoleDto updateRoleDto)
        {
            var user = await _userRepo.UpdateUserRole(userId, updateRoleDto.Roles);
            if (!user.Success)
            {
                return BadRequest(user);
            }

            return Ok(user);
        }

        [HttpPut("{userId}/active")]
        public async Task<ActionResult<Result<List<UserDto>>>> UpdateUserActiveState(string userId,
            UserUpdateActiveStateDto updateActiveStateDto)
        {
            var user = await _userRepo.UpdateUserActiveState(userId, updateActiveStateDto.IsActive);
            if (!user.Success)
            {
                return BadRequest(user);
            }

            return Ok(user);
        }
    }
}