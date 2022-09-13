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
        private readonly IGenerationService _generationService;

        public GenerationsController(IGenerationService generationService)
        {
            _generationService = generationService;
        }
        
        [HttpGet]
        [Authorize(Roles = $"{Roles.SuperAdmin}, {Roles.Admin}, {Roles.Teacher}")]
        public async Task<ActionResult<ServiceResponse<List<Generation>>>> GetGenerations()
        {
            var result = await _generationService.GetAllGenerations();
            return Ok(result);
        }
        
        [HttpGet("{id:int}")]
        [Authorize(Roles = $"{Roles.SuperAdmin}, {Roles.Admin}, {Roles.Teacher}")]
        public async Task<ActionResult<ServiceResponse<Generation>>> GetGenerationById(int id)
        {
            var result = await _generationService.GetGenerationById(id);
            return Ok(result);
        }
        
        [HttpPost]
        [Authorize(Roles = $"{Roles.SuperAdmin}, {Roles.Admin}, {Roles.Teacher}")]
        public async Task<ActionResult<ServiceResponse<Generation>>> AddGeneration(CreateGenerationDto request)
        {
            var result = await _generationService.AddGeneration(request);
            return Ok(result);
        }
        
        [HttpPut("{generationId:int}")]
        [Authorize(Roles = $"{Roles.SuperAdmin}, {Roles.Admin}, {Roles.Teacher}")]
        public async Task<ActionResult<ServiceResponse<Generation>>> UpdateGeneration(int generationId, UpdateGenerationDto generation)
        {
            var result = await _generationService.UpdateGeneration(generationId, generation);
            return Ok(result);
        }
        
        [HttpDelete("{id:int}")]
        [Authorize(Roles = $"{Roles.SuperAdmin}")]
        public async Task<ActionResult<ServiceResponse<Generation>>> DeleteGeneration(int id)
        {
            var result = await _generationService.DeleteGeneration(id);
            return Ok(result);
        }
    }
}
