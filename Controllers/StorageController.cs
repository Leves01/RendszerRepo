using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace RendszerRepo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StorageController : ControllerBase
    {
        private readonly IStorageService _storageService;
        public StorageController(IStorageService storageService) {
            _storageService = storageService;
        }

        [HttpGet("GetStorages")]
        public async Task<ActionResult<ServiceResponse<List<GetStoragesDto>>>> GetStorages() 
        {
            return Ok(await _storageService.GetStorages());
        }

        [HttpGet("GetStorageByPartId")]
        public async Task<ActionResult<ServiceResponse<List<GetStoragesDto>>>> GetStorageByPartId(int id) 
        {
            return Ok(await _storageService.GetStorageByPartId(id));
        }

        // [HttpGet("GetStorageByRow")]
        // public async Task<ActionResult<ServiceResponse<List<Storage>>>> GetStorageByRow(int row) 
        // {
        //     return Ok(await _storageService.GetStorageByRow(row));
        // }

        // [HttpGet("GetStorageByColumn")]
        // public async Task<ActionResult<ServiceResponse<List<Storage>>>> GetStorageByColumn(int column) 
        // {
        //     return Ok(await _storageService.GetStorageByColumn(column));
        // }

        // [HttpGet("GetStorageByDrawer")]
        // public async Task<ActionResult<ServiceResponse<List<Storage>>>> GetStorageByDrawer(int drawer) 
        // {
        //     return Ok(await _storageService.GetStorageByDrawer(drawer));
        // }

        [HttpPost("AddStorage")]
        public async Task<ActionResult<ServiceResponse<List<GetStoragesDto>>>> AddStorage(AddStorageDto newStorage) {
            return Ok(await _storageService.AddStorage(newStorage));
        }

        [HttpPut("UpdateStorage")]
        public async Task<ActionResult<ServiceResponse<List<GetStoragesDto>>>> UpdatePart(UpdateStoragesDto updatedStorage) {
            var response = await _storageService.UpdateStorage(updatedStorage);
            if(response.Data is null) {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("DeleteStorage")]
        public async Task<ActionResult<ServiceResponse<List<GetStoragesDto>>>> DeletePart(int id) 
        {
            var response = await _storageService.DeleteStorage(id);
            if(response.Data is null) {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}