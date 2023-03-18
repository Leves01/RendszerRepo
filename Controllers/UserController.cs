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

        [HttpGet("GetAll")]
        public ActionResult<List<User>> Get() 
        {
            return Ok(_userService.GetAllUsers());
        }

        [HttpGet("GetByRole")]
        public ActionResult<List<User>> GetByRole(Roles role) 
        {
            return Ok(_userService.GetUsersByRole(role));
        }

        [HttpPost]
        public ActionResult<List<User>> AddUser(User newUser) {
            return Ok(_userService.AddUser(newUser));
        }
    }
}