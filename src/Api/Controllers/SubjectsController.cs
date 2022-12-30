using Gbs.Application.Features.Subjects.Interfaces;
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
        public async Task<ActionResult<Result<List<SubjectResponse>>>> GetSubjects()
        {
            var result = await _subjectQueries.GetAll();
            return result.ToActionResult();
        }
        
        [HttpGet("{id:int}")]
        [Authorize(Policy = Policies.RequireAdminsSoundAndTeachers)]
        public async Task<ActionResult<Result<SubjectResponse>>> GetSubject(int id)
        {
            var result = await _subjectQueries.GetById(id);
            return result.ToActionResult();
        }
        
        [HttpPost]
        [Authorize(Policy = Policies.RequireAdmins)]
        public async Task<ActionResult<Result<SubjectResponse>>> AddSubject(CreateSubjectRequest subjectCreateDto)
        {
            var result = await _subjectCommands.Add(subjectCreateDto);
            return result.ToActionResult();
        }
        
        [HttpPut("{id:int}")]
        [Authorize(Policy = Policies.RequireAdmins)]
        public async Task<ActionResult<Result<SubjectResponse>>> UpdateSubject(int id, CreateSubjectRequest subjectUpdateDto)
        {
            var result = await _subjectCommands.Update(id, subjectUpdateDto);
            return result.ToActionResult();
        }
    }
}
