using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace RendszerRepo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartController : ControllerBase
    {
        private readonly IPartService _partService;
        public PartController(IPartService partService) {
            _partService = partService;
        }

        [HttpGet("GetAllParts")]
        public async Task<ActionResult<ServiceResponse<List<Part>>>> GetAllParts() 
        {
            return Ok(await _partService.GetAllParts());
        }

        [HttpPost("AddPart")]
        public async Task<ActionResult<ServiceResponse<List<Part>>>> AddPart(Part newPart) {
            return Ok(await _partService.AddPart(newPart));
        }

        [HttpPut("UpdatePart")]
        public async Task<ActionResult<ServiceResponse<List<Part>>>> UpdatePart(Part updatedPart) {
            var response = await _partService.UpdatePart(updatedPart);
            if(response.Data is null) {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("DeletePart")]
        public async Task<ActionResult<ServiceResponse<List<Part>>>> DeletePart(int id) 
        {
            var response = await _partService.DeletePart(id);
            if(response.Data is null) {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}