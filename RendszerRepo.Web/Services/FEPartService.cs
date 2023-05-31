using Newtonsoft.Json;
using RendszerRepo.Dtos.Part;
using RendszerRepo.Dtos.Storage;
using RendszerRepo.Models;
using RendszerRepo.Models.Dtos.Reserves;
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

        public Task<ServiceResponse<List<GetReserveDto>>> addReserves(AddReserveDto newReserve)
        {
            throw new NotImplementedException();
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

        public async Task<ServiceResponse<GetStoragesDto>> fillReserves(FillReservesDto fillReservesDto)
        {
            var response = new ServiceResponse<GetStoragesDto>();

            try
            {
                var httpResponse = await this.httpClient.PostAsJsonAsync("/api/Part/fillReserves", fillReservesDto);

                if (httpResponse.IsSuccessStatusCode)
                {
                    var content = await httpResponse.Content.ReadAsStringAsync();
                    response = JsonConvert.DeserializeObject<ServiceResponse<GetStoragesDto>>(content);
                }
                else if (httpResponse.StatusCode == HttpStatusCode.NotFound)
                {
                    response.Success = false;
                    response.Message = $"Reserve with Id '{fillReservesDto.ReservedId}' not found.";
                }
                else
                {
                    response.Success = false;
                    response.Message = $"An error occurred while filling reserves: {httpResponse.ReasonPhrase}";
                }
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

        public async Task<ServiceResponse<List<GetReserveDto>>> getReserves()
        {
            var reserves = await this.httpClient.GetFromJsonAsync<ServiceResponse<List<GetReserveDto>>>("api/Part/GetReserve");
            return reserves;
        }

        public async Task<ServiceResponse<GetPartDto>> PartToProject(PartToProjectDto newdto)
        {
            var response = await this.httpClient.PostAsJsonAsync("/api/Part/PartToProject", newdto);
            return await response.Content.ReadFromJsonAsync<ServiceResponse<GetPartDto>>();
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

        public Task<ServiceResponse<GetReserveDto>> updateReserve(UpdateReserveDto updateReserve)
        {
            throw new NotImplementedException();
        }
    }
}
