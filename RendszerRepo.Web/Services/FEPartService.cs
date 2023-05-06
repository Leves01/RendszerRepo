using Newtonsoft.Json;
using RendszerRepo.Dtos.Part;
using RendszerRepo.Models;
using RendszerRepo.Web.Services.Contracts;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace RendszerRepo.Web.Services
{
	public class FEPartService : FEIPartService
	{
		private readonly HttpClient httpClient;

		public FEPartService(HttpClient httpClient)
		{
			this.httpClient = httpClient;
		}

		public async Task<ServiceResponse<List<GetPartDto>>> AddPart(AddPartDto newPart)
		{
            var response = await this.httpClient.PostAsJsonAsync("/api/Part/AddPart", newPart);
            return await response.Content.ReadFromJsonAsync<ServiceResponse<List<GetPartDto>>>();
		}

		public async Task<ServiceResponse<List<GetPartDto>>> DeletePart(int id)
		{
            var response = new ServiceResponse<List<GetPartDto>>();
            try
            {
                HttpResponseMessage httpResponse = await this.httpClient.DeleteAsync($"/api/Part/DeletePart/{id}");
                string httpContent = await httpResponse.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

		public async Task<ServiceResponse<List<GetPartDto>>> GetAllParts()
		{

				var parts = await this.httpClient.GetFromJsonAsync<ServiceResponse<List<GetPartDto>>>("api/Part/GetAllParts");
				return parts;
		}

		public async Task<ServiceResponse<GetPartDto>> UpdatePart(UpdatePartDto updatedPart)
		{
            var response = await this.httpClient.PutAsJsonAsync("api/Part/UpdatePart", updatedPart);

            var result = new ServiceResponse<GetPartDto>();

            if (response.IsSuccessStatusCode)
            {
                result.Data = await response.Content.ReadFromJsonAsync<GetPartDto>();
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                result.Success = false;
                result.Message = errorMessage;
            }

            return result;
        }

		public Task<ServiceResponse<GetPartDto>> UpdatePartPrice(UpdatePartPriceDto updatedPart)
		{
			throw new NotImplementedException();
		}
	}
}
