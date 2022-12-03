using Gbs.Shared.Subjects;

namespace Gbs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectQueries _subjectQueries;
        private readonly ISubjectCommands _subjectCommands;

        public SubjectsController(ISubjectQueries subjectQueries, ISubjectCommands subjectCommands)
        {
            _subjectQueries = subjectQueries;
            _subjectCommands = subjectCommands;
        }
        
        [HttpGet]
        [Authorize(Policy = Policies.RequireAdminsSoundAndTeachers)]
        public async Task<ActionResult<Result<List<SubjectDto>>>> GetSubjects()
        {
            var result = await _subjectQueries.GetAll();
            return result.ToActionResult();
        }
        
        [HttpGet("{id:int}")]
        [Authorize(Policy = Policies.RequireAdminsSoundAndTeachers)]
        public async Task<ActionResult<Result<SubjectDto>>> GetSubject(int id)
        {
            var result = await _subjectQueries.GetById(id);
            return result.ToActionResult();
        }
        
        [HttpPost]
        [Authorize(Policy = Policies.RequireAdmins)]
        public async Task<ActionResult<Result<SubjectDto>>> AddSubject(SubjectCreateDto subjectCreateDto)
        {
            var result = await _subjectCommands.Add(subjectCreateDto);
            return result.ToActionResult();
        }
        
        [HttpPut("{id:int}")]
        [Authorize(Policy = Policies.RequireAdmins)]
        public async Task<ActionResult<Result<SubjectDto>>> UpdateSubject(int id, SubjectCreateDto subjectUpdateDto)
        {
            var result = await _subjectCommands.Update(id, subjectUpdateDto);
            return result.ToActionResult();
        }
    }
}
