using Gbs.Shared.Streams;

namespace Gbs.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StreamsController : ControllerBase
    {
        private readonly IStreamQueries _streamQueries;
        private readonly IStreamCommands _streamCommands;
        private readonly IHubContext<LivestreamHub> _streamHub;

        public StreamsController(IStreamQueries streamQueries, IStreamCommands streamCommands,
            IHubContext<LivestreamHub> streamHub)
        {
            _streamQueries = streamQueries;
            _streamCommands = streamCommands;
            _streamHub = streamHub;
        }

        [HttpGet]
        [Authorize(Policy = Policies.RequireAdminsSoundAndTeachers)]
        public async Task<ActionResult<Result<List<StreamDto>>>> GetStreams()
        {
            var response = await _streamQueries.GetAllStreams();
            return response.ToActionResult();
        }

        [HttpGet("{id:int}")]
        [Authorize(Policy = Policies.RequireAdminsSoundAndTeachers)]
        public async Task<ActionResult<Result<StreamDto>>> GetStream(int id)
        {
            var response = await _streamQueries.GetStreamById(id);
            return response.ToActionResult();
        }

        [HttpGet("{id:int}/live")]
        [Authorize(Policy = Policies.RequireAdminsSoundAndTeachers)]
        public async Task<ActionResult<Result<StreamDto>>> GetOnlineStream(int id)
        {
            var response = await _streamQueries.GetStreamById(id, true);
            return response.ToActionResult();
        }

        [HttpPost]
        [Authorize(Policy = Policies.RequireAdminsAndSound)]
        public async Task<ActionResult<Result<StreamDto>>> AddStream(StreamCreateDto streamCreateDto)
        {
            var response = await _streamCommands.CreateStream(streamCreateDto);
            await _streamHub.Clients.All.SendAsync("ReceiveStreams", await _streamQueries.GetAllStreams());
            return response.ToActionResult();
        }

        [HttpPut("{streamId:int}")]
        [Authorize(Policy = Policies.RequireAdminsAndSound)]
        public async Task<ActionResult<Result<StreamDto>>> UpdateStream(int streamId,
            StreamCreateDto liveDto)
        {
            var response = await _streamCommands.UpdateStream(streamId, liveDto);
            await _streamHub.Clients.All.SendAsync("ReceiveStreams", await _streamQueries.GetAllStreams());
            return response.ToActionResult();
        }

        [HttpPut("{streamId:int}/live")]
        [Authorize(Policy = Policies.RequireAdminsAndSound)]
        public async Task<ActionResult<Result<StreamDto>>> UpdateStreamLiveStatus(int streamId,
            StreamUpdateLiveDto liveDto)
        {
            var response = await _streamCommands.UpdateLiveStatus(streamId, liveDto);
            await _streamHub.Clients.All.SendAsync("ReceiveStreams", await _streamQueries.GetAllStreams());
            return response.ToActionResult();
        }

        [HttpDelete("{streamId:int}")]
        [Authorize(Policy = Policies.RequireAdminsAndSound)]
        public async Task<ActionResult<Result<bool>>> DeleteStream(int streamId)
        {
            var response = await _streamCommands.DeleteStream(streamId);
            await _streamHub.Clients.All.SendAsync("ReceiveStreams", await _streamQueries.GetAllStreams());
            return response.ToActionResult();
        }
    }
}