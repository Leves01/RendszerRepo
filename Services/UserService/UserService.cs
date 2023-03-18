using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RendszerRepo.Services.UserService
{
    public class UserService : IUserService
    {
        private static List<User> users = new List<User> {
            new User(),
            new User {username = "teszt", password = "teszt123", userRole = Roles.warehouseManager},
            new User {username = "ferencmeni", userRole = Roles.warehouseManager},
            new User {username = "balintmend", password = "wehehe", userRole = Roles.warehouseManager},
            new User {username = "1emp", userRole = Roles.warehouseEmployee},
            new User {username = "2emp", userRole = Roles.warehouseEmployee},
            new User {username = "tech1"}
        };
        public List<User> AddUser(User newUser)
        {
            users.Add(newUser);
            return users;
        }

        public List<User> GetAllUsers()
        {
            return users;
        }

        public List<User> GetUsersByRole(Roles role)
        {
            return users.FindAll(u => u.userRole == role);
        }
    }
}