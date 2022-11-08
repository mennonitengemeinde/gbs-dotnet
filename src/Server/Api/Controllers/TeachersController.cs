using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gbs.Server.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = Policies.RequireAdminsAndSound)]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherRepository _teacherRepo;

        public TeachersController(ITeacherRepository teacherRepo)
        {
            _teacherRepo = teacherRepo;
        }
        
        [HttpGet]
        public async Task<ActionResult<Result<List<TeacherDto>>>> GetAllTeachers()
        {
            var response = await _teacherRepo.GetTeachers();
            return Ok(response);
        }
        
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Result<TeacherDto>>> GetTeacher(int id)
        {
            var response = await _teacherRepo.GetTeacherById(id);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        
        [HttpPost]
        public async Task<ActionResult<Result<TeacherDto>>> AddTeacher(TeacherCreateDto teacherAddDto)
        {
            var response = await _teacherRepo.AddTeacher(teacherAddDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Result<TeacherDto>>> UpdateTeacher(int id, TeacherCreateDto teacherDto)
        {
            var response = await _teacherRepo.UpdateTeacher(id, teacherDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
