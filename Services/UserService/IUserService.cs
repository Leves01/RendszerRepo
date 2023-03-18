using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RendszerRepo.Services.UserService
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        List<User> GetUsersByRole(Roles role);
        List<User> AddUser(User newUser);
    }
}