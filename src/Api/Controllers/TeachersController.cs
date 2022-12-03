using Gbs.Shared.Teachers;

namespace Gbs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = Policies.RequireAdminsAndSound)]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherQueries _teacherQueries;
        private readonly ITeacherCommands _teacherCommands;

        public TeachersController(ITeacherQueries teacherQueries, ITeacherCommands teacherCommands)
        {
            _teacherQueries = teacherQueries;
            _teacherCommands = teacherCommands;
        }
        
        [HttpGet]
        public async Task<ActionResult<Result<List<TeacherDto>>>> GetAllTeachers()
        {
            var result = await _teacherQueries.GetAll();
            return result.ToActionResult();
        }
        
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Result<TeacherDto>>> GetTeacher(int id)
        {
            var result = await _teacherQueries.GetById(id);
            return result.ToActionResult();
        }
        
        [HttpPost]
        public async Task<ActionResult<Result<TeacherDto>>> AddTeacher(TeacherCreateDto teacherAddDto)
        {
            var result = await _teacherCommands.Add(teacherAddDto);
            return result.ToActionResult();
        }
        
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Result<TeacherDto>>> UpdateTeacher(int id, TeacherCreateDto teacherDto)
        {
            var result = await _teacherCommands.Update(id, teacherDto);
            return result.ToActionResult();
        }
        
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Result<bool>>> DeleteTeacher(int id)
        {
            var result = await _teacherCommands.Delete(id);
            return result.ToActionResult();
        }
    }
}
