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
        void Add(CardOwner user);
        void Update(CardOwner user);
        void Delete(Guid userId);
        IQueryable<CardOwner> GetAll();
    }
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public UserService(IUserRepository userRepository,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            ApplicationDbContext context)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _signInManager = signInManager;
            _context = context;
        }

        public void Add(CardOwner user)
        {
            _userRepository.Add(user);
            _context.SaveChanges();
        }

        public void Delete(Guid userId)
        {
            _userRepository.Delete(userId);
            _context.SaveChanges();
        }

        public IQueryable<CardOwner> GetAll()
        {
            return _userRepository.GetAll();
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

        public void Update(CardOwner user)
        {
            _userRepository.Update(user);
            _context.SaveChanges();
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
