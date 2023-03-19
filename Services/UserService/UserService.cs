using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RendszerRepo.Services.UserService
{
    public class UserService : IUserService
    { 
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public UserService(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration; 
        }

        // userId, username, password, userRole
        private static List<User> users = new List<User> {};
        public static User currentUser = new User();

        public async Task<ServiceResponse<List<User>>> AddUser(User newUser)
        {
            var serviceResponse = new ServiceResponse<List<User>>();
            var dbUsers = await _context.Users.ToListAsync();

            var addedUser = dbUsers.FirstOrDefault(u => u.username == newUser.username);
            if(addedUser is not null) {
                throw new Exception($"User with '{newUser.username}' username already exists.");
            }
            else {
                _context.Users.Add(newUser);
            }
            
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

        public async Task<ServiceResponse<string>> Login(string username, string password) {
            var serviceResponse = new ServiceResponse<string>();
            var dbUsers = await _context.Users.FirstOrDefaultAsync(u => ((u.username == username) && (u.password == password)));

            string token = CreateToken(currentUser);

            serviceResponse.Data = token;
            return serviceResponse;
        }

        public string CreateToken(User user) {
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!
            ));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

    }
}