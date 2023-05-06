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
        private readonly IMapper _mapper;

        public UserService(IMapper mapper, DataContext context, IConfiguration configuration)
        {
            _mapper = mapper;
            _context = context;
            _configuration = configuration; 
        }

        // userId, username, password, userRole
        private static List<User> users = new List<User> {};
        public static User currentUser = new User();

        public async Task<ServiceResponse<List<GetUserDto>>> AddUser(AddUserDto newUser)
        {
            var serviceResponse = new ServiceResponse<List<GetUserDto>>();
            // if(currentUser.userRole != Roles.admin) {
            //     throw new Exception($"You do not have permission to use this function");
            var dbUsers = await _context.Users.ToListAsync();

            var alreadyUser = dbUsers.FirstOrDefault(u => (u.username == newUser.username));
            // } else {
                if(alreadyUser is not null) {
                    throw new Exception($"User with '{newUser.username}' username already exists.");
                }
                else {
                    //.Add(_mapper.Map<AddUserDto>(newUser));
                    _context.Add(_mapper.Map<User>(newUser));
                }   
            // }
            await _context.SaveChangesAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> GetAllUsers()
        {
            var serviceResponse = new ServiceResponse<List<GetUserDto>>();
            var dbUsers = await _context.Users.ToListAsync();
            serviceResponse.Data = dbUsers.Select(u => _mapper.Map<GetUserDto>(u)).ToList();
            return serviceResponse;
        }

        {
        public async Task<ServiceResponse<GetUserDto>> GetUsersById(int id)
            var serviceResponse = new ServiceResponse<GetUserDto>();
            var dbUsers = await _context.Users.FirstOrDefaultAsync(u => u.userId == id);
            serviceResponse.Data = _mapper.Map<GetUserDto>(dbUsers);
            return serviceResponse;
        }

        public async Task<string> Login(string username, string password) {
            var currentUser = await _context.Users.FirstAsync(u => (u.username == username) && (u.password == password));

            string token = CreateToken(currentUser);

            return token;
        }

        public string CreateToken(User user) {

            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.username)
                ,new Claim(ClaimTypes.Role, user.userRole)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!
            ));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }

}