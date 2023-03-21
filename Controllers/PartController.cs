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
        public async Task<ActionResult<ServiceResponse<List<GetPartDto>>>> GetAllParts() 
        {
            return Ok(await _partService.GetAllParts());
        }

        [HttpPost("AddPart"), Authorize("WarehouseManager")]
        public async Task<ActionResult<ServiceResponse<List<GetPartDto>>>> AddPart(AddPartDto newPart) {
            return Ok(await _partService.AddPart(newPart));
        }

        [HttpPut("UpdatePartPrice"), Authorize("WarehouseManager")]
        public async Task<ActionResult<ServiceResponse<List<GetPartDto>>>> UpdatePartPrice(UpdatePartPriceDto updatedPart) {
            var response = await _partService.UpdatePartPrice(updatedPart);
            if(response.Data is null) {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPut("UpdatePart"), Authorize("WarehouseManager")]
        public async Task<ActionResult<ServiceResponse<List<GetPartDto>>>> UpdatePart(UpdatePartDto updatedPart) {
            var response = await _partService.UpdatePart(updatedPart);
            if(response.Data is null) {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("DeletePart"), Authorize("WarehouseManager")]
        public async Task<ActionResult<ServiceResponse<List<GetPartDto>>>> DeletePart(int id) 
        {
            var response = await _partService.DeletePart(id);
            if(response.Data is null) {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}