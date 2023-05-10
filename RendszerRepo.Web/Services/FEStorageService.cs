using RendszerRepo.Dtos.Part;
using RendszerRepo.Dtos.Storage;
using RendszerRepo.Models;
using RendszerRepo.Web.Pages;
using RendszerRepo.Web.Services.Contracts;
using System.Net.Http.Json;

namespace RendszerRepo.Web.Services
{
    public class FEStorageService : FEIStorageService
    {
        private readonly HttpClient httpClient;

        public FEStorageService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<ServiceResponse<List<GetStoragesDto>>> GetStorages()
        {
            var storages = await this.httpClient.GetFromJsonAsync<ServiceResponse<List<GetStoragesDto>>>("api/Storage/GetStorages");
            return storages;
        }

        public Task<ServiceResponse<List<GetStoragesDto>>> GetStorageByPartId(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<GetStoragesDto>>> AddStorage(AddStorageDto newStorage)
        {
            var response = await this.httpClient.PostAsJsonAsync("/api/Storage/AddStorage", newStorage);
            return await response.Content.ReadFromJsonAsync<ServiceResponse<List<GetStoragesDto>>>();
        }

        public async Task<ServiceResponse<List<GetStoragesDto>>> DeleteStorage(int id)
        {
            var response = new ServiceResponse<List<GetStoragesDto>>();
            try
            {
                HttpResponseMessage httpResponse = await this.httpClient.DeleteAsync($"/api/Storage/DeleteStorage/{id}");
                string httpContent = await httpResponse.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<GetStoragesDto>> UpdateMax(UpdateMaxDto updateMax)
        {
            var response = await this.httpClient.PutAsJsonAsync("api/Storage/UpdateMax", updateMax);

            var result = new ServiceResponse<GetStoragesDto>();

            if (response.IsSuccessStatusCode)
            {
                result.Data = await response.Content.ReadFromJsonAsync<GetStoragesDto>();
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                result.Success = false;
                result.Message = errorMessage;
            }

            return result;
        }

        public async Task<ServiceResponse<GetStoragesDto>> UpdateStorage(UpdateStoragesDto updatedStorage)
        {
            var response = await this.httpClient.PutAsJsonAsync("api/Storage/UpdateStorage", updatedStorage);

            var result = new ServiceResponse<GetStoragesDto>();

            if (response.IsSuccessStatusCode)
            {
                result.Data = await response.Content.ReadFromJsonAsync<GetStoragesDto>();
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
