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

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<ServiceResponse<List<User>>>> GetAllUsers() 
        {
            return Ok(await _userService.GetAllUsers());
        }

        [HttpGet("GetByRole")]
        public async Task<ActionResult<ServiceResponse<List<User>>>> GetByRole(Roles role) 
        {
            return Ok(await _userService.GetUsersByRole(role));
        }

        [HttpPost("AddUser")]
        public async Task<ActionResult<ServiceResponse<List<User>>>> AddUser(User newUser) {
            return Ok(await _userService.AddUser(newUser));
        }
    }
}