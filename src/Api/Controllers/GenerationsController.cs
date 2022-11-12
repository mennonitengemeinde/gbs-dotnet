namespace Gbs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GenerationsController : ControllerBase
    {
        private readonly IGenerationQueries _generationQueries;
        private readonly IGenerationCommands _generationCommands;

        public GenerationsController(IGenerationQueries generationQueries, IGenerationCommands generationCommands)
        {
            _generationQueries = generationQueries;
            _generationCommands = generationCommands;
        }

        [HttpGet]
        [Authorize(Policy = Policies.RequireAdminsAndSound)]
        public async Task<ActionResult<Result<List<GenerationDto>>>> GetGenerations()
        {
            var result = await _generationQueries.GetAll();
            return result.ToActionResult();
        }

        [HttpPost]
        [Authorize(Policy = Policies.RequireAdmins)]
        public async Task<ActionResult<Result<GenerationDto>>> AddGeneration(GenerationCreateDto request)
        {
            var result = await _generationCommands.Add(request);
            return result.ToActionResult();
        }

        [HttpGet("{id:int}")]
        [Authorize(Policy = Policies.RequireAdmins)]
        public async Task<ActionResult<Result<GenerationDto>>> GetGenerationById(int id)
        {
            var result = await _generationQueries.GetById(id);
            return result.ToActionResult();
        }

        [HttpPut("{generationId:int}")]
        [Authorize(Policy = Policies.RequireAdmins)]
        public async Task<ActionResult<Result<GenerationDto>>> UpdateGeneration(int generationId,
            GenerationUpdateDto generation)
        {
            var result = await _generationCommands.Update(generationId, generation);
            return result.ToActionResult();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Policy = Policies.RequireAdmins)]
        public async Task<ActionResult<Result<bool>>> DeleteGeneration(int id)
        {
            var result = await _generationCommands.Delete(id);
            return result.ToActionResult();
        }
    }
}