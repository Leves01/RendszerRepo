using RendszerRepo.Dtos.Project;
using RendszerRepo.Dtos.Project_properties;
using RendszerRepo.Models;
using RendszerRepo.Models.Dtos.Project;

namespace RendszerRepo.Web.Services.Contracts
{
    public interface FEIProjectService
    {
        Task<ServiceResponse<List<GetPrDto>>> AddProject(AddPrDto newProject);
        Task<ServiceResponse<List<GetPrDto>>> GetProjects();
        Task<ServiceResponse<GetProjectDto>> AddWorkTimeAndPrice(int projektid, int time, int price);
        Task<ServiceResponse<GetPrDto>> ProjectStatusChange(UpdateStatusDto newStatus);
    }
}
