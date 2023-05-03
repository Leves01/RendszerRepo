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
       Task<ServiceResponse<GetProjectDto>> AddWorkTimeAndPrice(int projektid, int time, int price);
       //Task<ServiceResponse<GetProjectDto>> PriceCalculation(int projektid, int time, int price);
       Task<ServiceResponse<GetProject_propertiesDto>> ProjectStatusChange(int projektid, string newstatus);
    }
}