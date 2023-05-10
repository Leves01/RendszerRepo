using RendszerRepo.Dtos.Part;
using RendszerRepo.Dtos.Project;
using RendszerRepo.Dtos.Project_properties;
using RendszerRepo.Dtos.Storage;
using RendszerRepo.Models;
using RendszerRepo.Models.Dtos.Project;
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
        public async Task<ServiceResponse<List<GetPrDto>>> AddProject(AddPrDto newProject)
        {
            var response = await this.httpClient.PostAsJsonAsync("/api/Project/AddProject", newProject);
            return await response.Content.ReadFromJsonAsync<ServiceResponse<List<GetPrDto>>>();
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

        public async Task<ServiceResponse<GetPrDto>> ProjectStatusChange(UpdateStatusDto newStatus)
        {
            var response = await this.httpClient.PutAsJsonAsync("api/Project/ProjectStatusChange", newStatus);

            var result = new ServiceResponse<GetPrDto>();

            if (response.IsSuccessStatusCode)
            {
                result.Data = await response.Content.ReadFromJsonAsync<GetPrDto>();
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                result.Success = false;
                result.Message = errorMessage;
            }

            return result;
        }
    }
}
