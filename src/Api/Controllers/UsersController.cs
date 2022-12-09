using Gbs.Application.Features.Identity.Interfaces;
using Gbs.Shared.Identity;

namespace Gbs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = Policies.RequireAdmins)]
    public class UsersController : ControllerBase
    {
        private readonly IIdentityQueries _identityQueries;
        private readonly IIdentityCommands _identityCommands;

        public UsersController(IIdentityQueries identityQueries, IIdentityCommands identityCommands)
        {
            _identityQueries = identityQueries;
            _identityCommands = identityCommands;
        }

        [HttpGet]
        public async Task<ActionResult<Result<List<UserResponse>>>> GetUsers()
        {
            var result = await _identityQueries.GetAll();
            return result.ToActionResult();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<Result<string>>> RegisterUser(RegisterRequest request)
        {
            var result = await _identityCommands.Add(request);
            return result.ToActionResult();
        }

        [HttpPut("{id}/church")]
        public async Task<ActionResult<Result<UserResponse>>> UpdateChurch(string id, UpdateUserChurchRequest updateDto)
        {
            var result = await _identityCommands.UpdateChurch(id, updateDto);
            return result.ToActionResult();
        }

        [HttpPut("{id}/roles")]
        public async Task<ActionResult<Result<UserResponse>>> UpdateRoles(string id,
            [FromBody] UpdateUserRoleRequest updateRoleDto)
        {
            var user = await _identityCommands.UpdateRoles(id, updateRoleDto.Roles);
            return user.ToActionResult();
        }

        [HttpPut("{id}/active")]
        public async Task<ActionResult<Result<UserResponse>>> UpdateUserActiveState(string id,
            UpdateUserActiveStateRequest updateActiveStateDto)
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