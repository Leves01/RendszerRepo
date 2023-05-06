using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using RendszerRepo.Models.Dtos.Login;

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

        [HttpGet("GetAllUsers"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> GetAllUsers() 
        {
            return Ok(await _userService.GetAllUsers());
        }

        [HttpGet("GetById"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> GetByRole(int id) 
        {
            return Ok(await _userService.GetUsersById(id));
        }

        [HttpPost("AddUser"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> AddUser(AddUserDto newUser) {
            return Ok(await _userService.AddUser(newUser));
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginDto user) {
            return Ok(await _userService.Login(user.Username, user.Password));
        }

    }
}