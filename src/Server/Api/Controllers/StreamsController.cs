using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gbs.Server.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StreamsController : ControllerBase
    {
        private readonly IStreamRepository _streamRepo;

        public StreamsController(IStreamRepository streamRepo)
        {
            _streamRepo = streamRepo;
        }

        [HttpGet]
        [Authorize(Policy = Policies.RequireAdminsSoundAndTeachers)]
        public async Task<ActionResult<Result<List<StreamDto>>>> GetStreams()
        {
            var response = await _streamRepo.GetLiveStreams();
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
        
        [HttpGet("{id:int}")]
        [Authorize(Policy = Policies.RequireAdminsSoundAndTeachers)]
        public async Task<ActionResult<Result<StreamDto>>> GetStream(int id)
        {
            var response = await _streamRepo.GetLiveStreamById(id);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
        
        [HttpGet("{id:int}/live")]
        [Authorize(Policy = Policies.RequireAdminsSoundAndTeachers)]
        public async Task<ActionResult<Result<StreamDto>>> GetOnlineStream(int id)
        {
            var response = await _streamRepo.GetLiveStreamById(id, true);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost]
        [Authorize(Policy = Policies.RequireAdminsAndSound)]
        public async Task<ActionResult<Result<StreamDto>>> AddStream(StreamCreateDto streamCreateDto)
        {
            var response = await _streamRepo.CreateLiveStream(streamCreateDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
        
        [HttpPut("{streamId:int}")]
        [Authorize(Policy = Policies.RequireAdminsAndSound)]
        public async Task<ActionResult<Result<StreamDto>>> UpdateStream(int streamId,
            StreamCreateDto liveDto)
        {
            var response = await _streamRepo.UpdateStream(streamId, liveDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("{streamId:int}/live")]
        [Authorize(Policy = Policies.RequireAdminsAndSound)]
        public async Task<ActionResult<Result<StreamDto>>> UpdateStreamLiveStatus(int streamId,
            StreamUpdateLiveDto liveDto)
        {
            var response = await _streamRepo.UpdateStreamLiveStatus(streamId, liveDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
        
        [HttpDelete("{streamId:int}")]
        [Authorize(Policy = Policies.RequireAdminsAndSound)]
        public async Task<ActionResult<Result<int>>> DeleteStream(int streamId)
        {
            var response = await _streamRepo.DeleteStream(streamId);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}