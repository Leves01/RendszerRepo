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
        public ProjectController(IProjectService ProjectService) {
            _projectService=ProjectService;
        }

        [HttpPost("AddProject")]
        public async Task<ActionResult<ServiceResponse<List<GetPrDto>>>> AddProject(AddPrDto newProject) 
        {
            return Ok(await _projectService.AddProject(newProject));
        }

        [HttpGet("GetProject")]
        public async Task<ActionResult<ServiceResponse<List<GetProject_propertiesDto>>>> GetProject() 
        {
            return Ok(await _projectService.GetProjects());
        }
    }
}