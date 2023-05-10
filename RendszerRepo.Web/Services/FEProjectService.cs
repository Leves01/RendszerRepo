using RendszerRepo.Dtos.Part;
using RendszerRepo.Dtos.Project;
using RendszerRepo.Dtos.Project_properties;
using RendszerRepo.Models;
using RendszerRepo.Web.Services.Contracts;
using System.Net.Http.Json;

namespace RendszerRepo.Web.Services
{
    public class FEProjectService : FEIProjectService
    {
        private readonly HttpClient httpClient;

        public FEProjectService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public Task<ServiceResponse<List<GetPrDto>>> AddProject(AddPrDto newProject)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<GetProjectDto>> AddWorkTimeAndPrice(int projektid, int time, int price)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<GetPrDto>>> GetProjects()
        {
            var projects = await this.httpClient.GetFromJsonAsync<ServiceResponse<List<GetPrDto>>>("api/Project/GetProject");
            return projects;
        }

        public Task<ServiceResponse<GetProject_propertiesDto>> ProjectStatusChange(int projektid, string newstatus)
        {
            throw new NotImplementedException();
        }
    }
}
