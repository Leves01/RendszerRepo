using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace RendszerRepo.Services.ProjectService
{
    public interface IProjectService
    {
        Task<ServiceResponse<List<GetProject_propertiesDto>>> AddProject(AddProjectDto newProject);
        Task<ServiceResponse<List<GetProject_propertiesDto>>> GetProjects();
    }
}