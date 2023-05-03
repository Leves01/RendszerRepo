using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RendszerRepo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        public ProjectController(IProjectService projectService) {
            _projectService = projectService;
        }

        [HttpPut("AddWorkTimeAndPrice"), Authorize(Roles = "Technician")]
        public async Task<ActionResult<ServiceResponse<List<GetProjectDto>>>> AddWorkTimeAndPrice(int projektid, int time, int price) 
        {
            var response = await _projectService.AddWorkTimeAndPrice(projektid, time, price);
            if(response.Data is null) {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost("AddProject"), Authorize(Roles = "Technician")]
        public async Task<ActionResult<ServiceResponse<List<GetProject_propertiesDto>>>> AddProject(AddProjectDto newProject) 
        {
            return Ok(await _projectService.AddProject(newProject));
        }

        [HttpGet("GetProject"), Authorize(Roles = "Technician")]
        public async Task<ActionResult<ServiceResponse<List<GetProject_propertiesDto>>>> GetProject() 
        {
            return Ok(await _projectService.GetProjects());
        }

        [HttpPost("ProjectStatusChange"), Authorize(Roles = "Technician")]
        public async Task<ActionResult<ServiceResponse<GetProject_propertiesDto>>> ProjectStatusChange(int projektid, string newstatus) 
        {
            return Ok(await _projectService.ProjectStatusChange(projektid,newstatus));
        }
    }
}