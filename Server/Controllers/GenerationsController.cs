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
    public class GenerationsController : ControllerBase
    {
        private readonly IGenerationRepository _generationRepo;

        public GenerationsController(IGenerationRepository generationRepo)
        {
            _generationRepo = generationRepo;
        }
        
        [HttpGet]
        [Authorize(Roles = $"{Roles.SuperAdmin}, {Roles.Admin}, {Roles.Teacher}")]
        public async Task<ActionResult<ServiceResponse<List<Generation>>>> GetGenerations()
        {
            var result = await _generationRepo.GetAllGenerations();
            return Ok(result);
        }
        
        [HttpGet("{id:int}")]
        [Authorize(Roles = $"{Roles.SuperAdmin}, {Roles.Admin}, {Roles.Teacher}")]
        public async Task<ActionResult<ServiceResponse<Generation>>> GetGenerationById(int id)
        {
            var result = await _generationRepo.GetGenerationById(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        
        [HttpPost]
        [Authorize(Roles = $"{Roles.SuperAdmin}, {Roles.Admin}, {Roles.Teacher}")]
        public async Task<ActionResult<ServiceResponse<Generation>>> AddGeneration(GenerationCreateDto request)
        {
            var result = await _generationRepo.AddGeneration(request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        
        [HttpPut("{generationId:int}")]
        [Authorize(Roles = $"{Roles.SuperAdmin}, {Roles.Admin}, {Roles.Teacher}")]
        public async Task<ActionResult<ServiceResponse<Generation>>> UpdateGeneration(int generationId, GenerationUpdateDto generation)
        {
            var result = await _generationRepo.UpdateGeneration(generationId, generation);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        
        [HttpDelete("{id:int}")]
        [Authorize(Roles = $"{Roles.SuperAdmin}")]
        public async Task<ActionResult<ServiceResponse<Generation>>> DeleteGeneration(int id)
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
