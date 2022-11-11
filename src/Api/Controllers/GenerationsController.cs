namespace Gbs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GenerationsController : ControllerBase
    {
        private readonly IGenerationRepository _generationRepo;

        public GenerationsController(IGenerationRepository generationRepo)
        {
            _generationRepo = generationRepo;
        }
        
        [HttpGet]
        [Authorize(Policy = Policies.RequireAdminsAndSound)]
        public async Task<ActionResult<Result<List<GenerationDto>>>> GetGenerations()
        {
            var result = await _generationRepo.GetAllGenerations();
            return Ok(result);
        }
        
        [HttpGet("{id:int}")]
        [Authorize(Policy = Policies.RequireAdmins)]
        public async Task<ActionResult<Result<GenerationDto>>> GetGenerationById(int id)
        {
            var result = await _generationRepo.GetGenerationById(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        
        [HttpPost]
        [Authorize(Policy = Policies.RequireAdmins)]
        public async Task<ActionResult<Result<GenerationDto>>> AddGeneration(GenerationCreateDto request)
        {
            var result = await _generationRepo.AddGeneration(request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        
        [HttpPut("{generationId:int}")]
        [Authorize(Policy = Policies.RequireAdmins)]
        public async Task<ActionResult<Result<GenerationDto>>> UpdateGeneration(int generationId, GenerationUpdateDto generation)
        {
            var result = await _generationRepo.UpdateGeneration(generationId, generation);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        
        [HttpDelete("{id:int}")]
        [Authorize(Policy = Policies.RequireAdmins)]
        public async Task<ActionResult<Result<bool>>> DeleteGeneration(int id)
        {
            var result = await _generationRepo.DeleteGeneration(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
