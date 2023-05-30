using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RendszerRepo.Models.Dtos.Project;

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
                return NotFound(response);
            if(response.Data is null) {
            }
            return Ok(response);
        }

        [HttpPost("AddProject")]
        public async Task<ActionResult<ServiceResponse<List<GetPrDto>>>> AddProject(AddPrDto newProject) 
        {
            return Ok(await _projectService.AddProject(newProject));
        }

        [HttpGet("GetProject"), Authorize(Roles = "Technician")]
        public async Task<ActionResult<ServiceResponse<List<GetProject_propertiesDto>>>> GetProject() 
        {
            return Ok(await _projectService.GetProjects());
        }

        [HttpPost("ProjectStatusChange"), Authorize(Roles = "Technician")]
        public async Task<ActionResult<ServiceResponse<GetProject_propertiesDto>>> ProjectStatusChange(UpdateStatusDto newStatus) 
        {
            return Ok(await _projectService.ProjectStatusChange(newStatus));
        }
         [HttpPost("Add Project Properties")]
        public async Task<ActionResult<ServiceResponse<GetProject_propertiesDto>>> ProjectStatusChange(AddProjectDto newProjectPr) 
        {
            return Ok(await _projectService.AddProject_properties(newProjectPr));
        }
    }
}