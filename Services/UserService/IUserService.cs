using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace RendszerRepo.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<List<GetUserDto>>> GetAllUsers();
        Task<ServiceResponse<GetUserDto>> GetUsersById(int id);
        Task<ServiceResponse<List<GetUserDto>>> AddUser(AddUserDto newUser);
        Task<ServiceResponse<string>> Login(string username, string password);
        public string CreateToken(User user);
    }
}