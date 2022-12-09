using Gbs.Application.Features.Streams.Interfaces;
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
        public async Task<ActionResult<Result<List<StreamResponse>>>> GetStreams()
        {
            var response = await _streamQueries.GetAllStreams();
            return response.ToActionResult();
        }

        [HttpGet("{id:int}")]
        [Authorize(Policy = Policies.RequireAdminsSoundAndTeachers)]
        public async Task<ActionResult<Result<StreamResponse>>> GetStream(int id)
        {
            var response = await _streamQueries.GetStreamById(id);
            return response.ToActionResult();
        }

        [HttpGet("{id:int}/live")]
        [Authorize(Policy = Policies.RequireAdminsSoundAndTeachers)]
        public async Task<ActionResult<Result<StreamResponse>>> GetOnlineStream(int id)
        {
            var response = await _streamQueries.GetStreamById(id, true);
            return response.ToActionResult();
        }

        [HttpPost]
        [Authorize(Policy = Policies.RequireAdminsAndSound)]
        public async Task<ActionResult<Result<StreamResponse>>> AddStream(CreateStreamRequest streamCreateDto)
        {
            var response = await _streamCommands.CreateStream(streamCreateDto);
            await _streamHub.Clients.All.SendAsync("ReceiveStreams", await _streamQueries.GetAllStreams());
            return response.ToActionResult();
        }

        [HttpPut("{streamId:int}")]
        [Authorize(Policy = Policies.RequireAdminsAndSound)]
        public async Task<ActionResult<Result<StreamResponse>>> UpdateStream(int streamId,
            UpdateStreamRequest liveDto)
        {
            var response = await _streamCommands.UpdateStream(streamId, liveDto);
            await _streamHub.Clients.All.SendAsync("ReceiveStreams", await _streamQueries.GetAllStreams());
            return response.ToActionResult();
        }

        [HttpPut("{streamId:int}/live")]
        [Authorize(Policy = Policies.RequireAdminsAndSound)]
        public async Task<ActionResult<Result<StreamResponse>>> UpdateStreamLiveStatus(int streamId,
            UpdateStreamLiveRequest liveDto)
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