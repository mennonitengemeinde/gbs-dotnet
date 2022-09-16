using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace gbs.Server.Controllers
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
        public async Task<ActionResult<ServiceResponse<List<LiveStream>>>> GetStreams()
        {
            var response = await _streamRepo.GetLiveStreams();
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
        
        [HttpGet("{id:int}/live")]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<LiveStream>>> GetStream(int id)
        {
            var response = await _streamRepo.GetOnlineLiveStreamById(id);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = $"{Roles.SuperAdmin}, {Roles.Admin}")]
        public async Task<ActionResult<ServiceResponse<LiveStream>>> AddStream(StreamCreateDto streamCreateDto)
        {
            var response = await _streamRepo.CreateLiveStream(streamCreateDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("{streamId:int}/live")]
        [Authorize(Roles = $"{Roles.SuperAdmin}, {Roles.Admin}")]
        public async Task<ActionResult<ServiceResponse<LiveStream>>> UpdateStreamLiveStatus(int streamId,
            StreamUpdateLiveDto liveDto)
        {
            var response = await _streamRepo.UpdateStreamLiveStatus(streamId, liveDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}