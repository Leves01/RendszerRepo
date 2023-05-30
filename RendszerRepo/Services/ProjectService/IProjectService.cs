using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RendszerRepo.Models.Dtos.Project;


namespace RendszerRepo.Services.ProjectService
{
    public interface IProjectService
    {
        Task<ServiceResponse<List<GetPrDto>>> AddProject(AddPrDto newProject);
        Task<ServiceResponse<List<GetPrDto>>> GetProjects();
       Task<ServiceResponse<GetProjectDto>> AddWorkTimeAndPrice(int projektid, int time, int price);
       Task<ServiceResponse<List<GetProject_propertiesDto>>> AddProject_properties(AddProjectDto newProjectPr);
       //Task<ServiceResponse<GetProjectDto>> PriceCalculation(int projektid, int time, int price);
       Task<ServiceResponse<GetProject_propertiesDto>> ProjectStatusChange(UpdateStatusDto newStatus);
    }
}