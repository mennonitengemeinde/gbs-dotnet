using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace gbs.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = $"{Roles.Admin}, {Roles.SuperAdmin}")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<UserDto>>>> GetUsers()
        {
            var users = await _userService.GetUsers();
            return Ok(users);
        }

        [HttpPut("{userId:int}/role")]
        public async Task<ActionResult<ServiceResponse<UserDto>>> UpdateUserRole(int userId,
            [FromBody] UserUpdateRoleDto updateRoleDto)
        {
            var user = await _userService.UpdateUserRole(userId, updateRoleDto.Role);
            return Ok(user);
        }
    }
}