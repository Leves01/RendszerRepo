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

        // userId, username, password, userRole
        private static List<User> users = new List<User> {};

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