using Gbs.Application.Features.Lessons.Interfaces;
using Gbs.Shared.Lessons;

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
        public async Task<ActionResult<Result<List<LessonResponse>>>> GetAll()
        {
            var result = await _lessonQueries.GetAll();
            return result.ToActionResult();
        }

        [HttpGet("{id:int}")]
        [Authorize(Policy = Policies.RequireAdminsSoundAndTeachers)]
        public async Task<ActionResult<Result<LessonResponse>>> Get(int id)
        {
            var result = await _lessonQueries.GetById(id);
            return result.ToActionResult();
        }

        [HttpPost]
        [Authorize(Policy = Policies.RequireAdminsAndSound)]
        public async Task<ActionResult<Result<LessonResponse>>> Create(CreateLessonRequest dto)
        {
            var result = await _lessonCommands.Add(dto);
            return result.ToActionResult();
        }

        [HttpPut("{id:int}")]
        [Authorize(Policy = Policies.RequireAdminsAndSound)]
        public async Task<ActionResult<Result<LessonResponse>>> Update(int id, UpdateLessonRequest dto)
        {
            var result = await _lessonCommands.Update(dto);
            return result.ToActionResult();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Policy = Policies.RequireAdmins)]
        public async Task<ActionResult<Result<bool>>> Delete(int id)
        {
            var result = await _lessonCommands.Delete(id);
            return result.ToActionResult();
        }
        
        [HttpPut("{id:int}/order")]
        [Authorize(Policy = Policies.RequireAdminsAndSound)]
        public async Task<ActionResult<Result<LessonResponse>>> UpdateOrder(int id, [FromBody] int order)
        {
            var result = await _lessonCommands.UpdateOrder(id, order);
            return result.ToActionResult();
        }
    }
}