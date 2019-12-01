using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SAD.Model;
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
        private readonly IMapper _mapper;

        public UserController(IConfiguration configuration, IUserService userService, IMapper mapper)
        {
            _configuration = configuration;
            _userService = userService;
            _mapper = mapper;
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

        [HttpGet()]
        public IActionResult GetAll()
        {
            return Ok(_userService.GetAll().ProjectTo<UserVM>(_mapper.ConfigurationProvider));
        }

        [HttpPost()]
        public IActionResult Save(UserVM user)
        {
            _userService.Add(_mapper.Map<CardOwner>(user));
            return Ok();
        }
        [HttpPut()]
        public IActionResult Update(UserVM user)
        {
            _userService.Update(_mapper.Map<CardOwner>(user));
            return Ok();
        }
    }
}
