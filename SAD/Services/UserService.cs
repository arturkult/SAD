using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SAD.Helpers;
using SAD.Model;
using SAD.Repository;
using SAD.ViewModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SAD.Services
{
    public interface IUserService
    {
        string Login(LoginVM user);
    }
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserService(IUserRepository userRepository,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _signInManager = signInManager;
        }
        public string Login(LoginVM user)
        {
            var userFromDb = _userRepository.GetUserByEmail(user.Email);
            if (userFromDb == null)
            {
                return string.Empty;
            }
            if (_signInManager.PasswordSignInAsync(userFromDb, user.Password, false, false).Result.Succeeded)
            {
                return GenerateToken();
            }
            return string.Empty;
        }
        private string GenerateToken()
        {
            var securitySection = _configuration.GetSection("Security").Get<SecuritySection>();
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securitySection.SecretKey));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: securitySection.Issuer,
                audience: securitySection.Audience,
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signingCredentials
                );
             
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
    }

}
