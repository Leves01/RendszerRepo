using RendszerRepo.Dtos.Project;
using RendszerRepo.Dtos.Project_properties;
using RendszerRepo.Models;

namespace RendszerRepo.Web.Services.Contracts
{
    public interface FEIProjectService
    {
        Task<ServiceResponse<List<GetPrDto>>> AddProject(AddPrDto newProject);
        Task<ServiceResponse<List<GetPrDto>>> GetProjects();
        Task<ServiceResponse<GetProjectDto>> AddWorkTimeAndPrice(int projektid, int time, int price);
        Task<ServiceResponse<GetProject_propertiesDto>> ProjectStatusChange(int projektid, string newstatus);
    }
}
