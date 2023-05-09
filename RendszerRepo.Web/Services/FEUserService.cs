using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using RendszerRepo.Dtos.User;
using RendszerRepo.Models;
using RendszerRepo.Models.Dtos.Login;
using RendszerRepo.Web.Services.Contracts;

namespace RendszerRepo.Services
{
	public class FEUserService : FEIUserService
	{
		private readonly HttpClient _httpClient;

		public FEUserService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		public async Task<ServiceResponse<List<GetUserDto>>> GetAllUsers(string jwtToken)
		{
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

			var usersResponse = await _httpClient.GetAsync("api/User/GetAllUsers");
			var users = await usersResponse.Content.ReadFromJsonAsync<ServiceResponse<List<GetUserDto>>>();
			return users;
		}

		public async Task<ServiceResponse<GetUserDto>> GetUsersById(int id, string jwtToken)
		{
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

			var usersResponse = await _httpClient.GetAsync($"api/User/GetById?id={id}");
			var user = await usersResponse.Content.ReadFromJsonAsync<GetUserDto>();
			var response = new ServiceResponse<GetUserDto>();
			response.Data = user;
			return response;
		}

		public async Task<ServiceResponse<List<GetUserDto>>> AddUser(AddUserDto newUser, string jwtToken)
		{
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

			var response = await _httpClient.PostAsJsonAsync("api/User/AddUser", newUser);
			var result = await response.Content.ReadFromJsonAsync<ServiceResponse<List<GetUserDto>>>();
			return result;
		}

		public async Task<string> LoginAsync(string username, string password)
		{

            var requestData = new Dictionary<string, string> {
				{ "username", username },
				{ "password", password }
			};

            var requestContent = new FormUrlEncodedContent(requestData);
            var response = await _httpClient.PostAsync("api/User/login", requestContent);

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                throw new ApplicationException($"Error: {errorResponse}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            return responseContent;

        }
	}
}
