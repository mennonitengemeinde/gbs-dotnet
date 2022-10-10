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
    [Authorize(Policy = Policies.RequireAdmins)]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepo;

        public UsersController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<UserDto>>>> GetUsers()
        {
            var users = await _userRepo.GetUsers();
            return Ok(users);
        }

        [HttpPut("{userId:int}/church")]
        public async Task<ActionResult<ServiceResponse<List<UserDto>>>> UpdateUser(int userId, UserUpdateChurchDto updateDto)
        {
            var user = await _userRepo.UpdateUserChurch(userId, updateDto);
            if (!user.Success)
            {
                return BadRequest(user);
            }

            return Ok(user);
        }

        [HttpPut("{userId:int}/role")]
        public async Task<ActionResult<ServiceResponse<List<UserDto>>>> UpdateUserRole(int userId,
            [FromBody] UserUpdateRoleDto updateRoleDto)
        {
            var user = await _userRepo.UpdateUserRole(userId, updateRoleDto.Role);
            if (!user.Success)
            {
                return BadRequest(user);
            }

            return Ok(user);
        }

        [HttpPut("{userId:int}/active")]
        public async Task<ActionResult<ServiceResponse<List<UserDto>>>> UpdateUserActiveState(int userId,
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