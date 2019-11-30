using SAD.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAD.Repository
{
    public interface IUserRepository
    {
        ApplicationUser GetUserByEmail(string email);
    }
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public ApplicationUser GetUserByEmail(string email)
        {
            return _context.Users.Where(user => user.Email.Equals(email)).FirstOrDefault();
        }
    }
}
