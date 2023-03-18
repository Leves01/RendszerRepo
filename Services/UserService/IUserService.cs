using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RendszerRepo.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<List<User>>> GetAllUsers();
        Task<ServiceResponse<List<User>>> GetUsersByRole(Roles role);
        Task<ServiceResponse<List<User>>> AddUser(User newUser);
        
        // Task<ServiceResponse<List<User>>> loginUser(User user);
    }
}