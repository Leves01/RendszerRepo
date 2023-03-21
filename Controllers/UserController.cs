using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;


namespace RendszerRepo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAllUsers"), Authorize("Admin")]
        public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> GetAllUsers() 
        {
            return Ok(await _userService.GetAllUsers());
        }

        [HttpGet("GetById"), Authorize("Admin")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> GetByRole(int id) 
        {
            return Ok(await _userService.GetUsersById(id));
        }

        [HttpPost("AddUser"), Authorize("Admin")]
        public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> AddUser(AddUserDto newUser) {
            return Ok(await _userService.AddUser(newUser));
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(string username, string password) {
            return Ok(await _userService.Login(username, password));
        }

    }
}