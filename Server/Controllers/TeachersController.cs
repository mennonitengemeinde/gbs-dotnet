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
    [Authorize]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherRepository _teacherRepo;

        public TeachersController(ITeacherRepository teacherRepo)
        {
            _teacherRepo = teacherRepo;
        }
        
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Teacher>>>> GetAllTeachers()
        {
            var response = await _teacherRepo.GetTeachers();
            return Ok(response);
        }
        
        [HttpPost]
        [Authorize(Roles = $"{Roles.SuperAdmin}, {Roles.Admin}, {Roles.Teacher}")]
        public async Task<ActionResult<ServiceResponse<Teacher>>> AddTeacher(TeacherCreateDto teacherAddDto)
        {
            var response = await _teacherRepo.AddTeacher(teacherAddDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
