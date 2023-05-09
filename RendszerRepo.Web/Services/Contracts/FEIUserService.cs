using RendszerRepo.Dtos.User;
using RendszerRepo.Models;
using RendszerRepo.Models.Dtos.Login;

namespace RendszerRepo.Web.Services.Contracts
{
	public interface FEIUserService
	{
		Task<ServiceResponse<List<GetUserDto>>> GetAllUsers(string jwtToken);
		Task<ServiceResponse<GetUserDto>> GetUsersById(int id, string jwtToken);
		Task<ServiceResponse<List<GetUserDto>>> AddUser(AddUserDto newUser, string jwtToken);
		Task<string> LoginAsync(string username, string password);
	}
}
