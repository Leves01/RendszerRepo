using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RendszerRepo.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        public UserService(DataContext context)
        {
            _context = context;
        }

        private static List<User> users = new List<User> {
            // new User(),
            // new User {username = "admin", password = "admin", userRole = Roles.admin},
            // new User {username = "teszt", password = "teszt123", userRole = Roles.warehouseManager},
            // new User {username = "ferencmeni", userRole = Roles.warehouseManager},
            // new User {username = "balintmend", password = "wehehe", userRole = Roles.warehouseManager},
            // new User {username = "1emp", userRole = Roles.warehouseEmployee},
            // new User {username = "2emp", userRole = Roles.warehouseEmployee},
            // new User {username = "tech1"}
        };

        public async Task<ServiceResponse<List<User>>> AddUser(User newUser)
        {
            var serviceResponse = new ServiceResponse<List<User>>();
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<User>>> GetAllUsers()
        {
            var serviceResponse = new ServiceResponse<List<User>>();
            var dbUsers = await _context.Users.ToListAsync();
            serviceResponse.Data = dbUsers;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<User>>> GetUsersByRole(Roles role)
        {
            var serviceResponse = new ServiceResponse<List<User>>();
            var dbUsers = await _context.Users.ToListAsync();
            var user = dbUsers.FindAll(u => u.userRole == role);
            serviceResponse.Data = user;
            return serviceResponse;
        }
    }
}