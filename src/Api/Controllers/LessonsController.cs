namespace Gbs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly ILessonQueries _lessonQueries;
        private readonly ILessonCommands _lessonCommands;

        public LessonsController(ILessonQueries lessonQueries, ILessonCommands lessonCommands)
        {
            _lessonQueries = lessonQueries;
            _lessonCommands = lessonCommands;
        }

        [HttpGet]
        [Authorize(Policy = Policies.RequireAdminsSoundAndTeachers)]
        public async Task<ActionResult<Result<List<LessonDto>>>> GetAll()
        {
            var result = await _lessonQueries.GetAll();
            return result.ToActionResult();
        }

        [HttpGet("{id:int}")]
        [Authorize(Policy = Policies.RequireAdminsSoundAndTeachers)]
        public async Task<ActionResult<Result<LessonDto>>> Get(int id)
        {
            var result = await _lessonQueries.GetById(id);
            return result.ToActionResult();
        }

        [HttpPost]
        [Authorize(Policy = Policies.RequireAdminsAndSound)]
        public async Task<ActionResult<Result<LessonDto>>> Create(LessonCreateDto dto)
        {
            var result = await _lessonCommands.Add(dto);
            return result.ToActionResult();
        }

        [HttpPut("{id:int}")]
        [Authorize(Policy = Policies.RequireAdminsAndSound)]
        public async Task<ActionResult<Result<LessonDto>>> Update(int id, LessonCreateDto dto)
        {
            var result = await _lessonCommands.Update(id, dto);
            return result.ToActionResult();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Policy = Policies.RequireAdmins)]
        public async Task<ActionResult<Result<bool>>> Delete(int id)
        {
            var result = await _lessonCommands.Delete(id);
            return result.ToActionResult();
        }
    }
}