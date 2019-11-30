using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SAD.Services;
using SAD.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAD.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public UserController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginVM user)
        {
            if (user == null
                || user.Email == null
                || user.Password == null)
            {
                return BadRequest();
            }
            var result = _userService.Login(user);
            if(result.Equals(string.Empty))
            {
                return BadRequest();
            }
            return Ok(new {
                Token = result
            });
        }
    }
}
