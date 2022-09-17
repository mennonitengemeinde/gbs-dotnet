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
        public async Task<ActionResult<ServiceResponse<List<StreamGetDto>>>> GetStreams()
        {
            var response = await _streamRepo.GetLiveStreams();
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
        
        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<StreamGetDto>>> GetStream(int id)
        {
            var response = await _streamRepo.GetLiveStreamById(id);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
        
        [HttpGet("{id:int}/live")]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<StreamGetDto>>> GetOnlineStream(int id)
        {
            var response = await _streamRepo.GetLiveStreamById(id, true);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = $"{Roles.SuperAdmin}, {Roles.Admin}")]
        public async Task<ActionResult<ServiceResponse<StreamGetDto>>> AddStream(StreamCreateDto streamCreateDto)
        {
            var response = await _streamRepo.CreateLiveStream(streamCreateDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
        
        [HttpPut("{streamId:int}")]
        [Authorize(Roles = $"{Roles.SuperAdmin}, {Roles.Admin}")]
        public async Task<ActionResult<ServiceResponse<StreamGetDto>>> UpdateStream(int streamId,
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
        [Authorize(Roles = $"{Roles.SuperAdmin}, {Roles.Admin}")]
        public async Task<ActionResult<ServiceResponse<StreamGetDto>>> UpdateStreamLiveStatus(int streamId,
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
        [Authorize(Roles = $"{Roles.SuperAdmin}, {Roles.Admin}")]
        public async Task<ActionResult<ServiceResponse<int>>> DeleteStream(int streamId)
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