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
        void Add(CardOwner user);
        void Update(CardOwner user);
        void Delete(Guid userId);
        IQueryable<CardOwner> GetAll();
    }
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(CardOwner user)
        {
            _context.Add(user);
        }

        public void Delete(Guid userId)
        {
            var user = _context.CardOwners.FirstOrDefault(u => u.Id.Equals(userId));
            if(user == null)
            {
                throw new NullReferenceException();
            }
            _context.Remove(user);
        }

        public IQueryable<CardOwner> GetAll()
        {
            return _context.CardOwners;
        }

        public ApplicationUser GetUserByEmail(string email)
        {
            return _context.Users.Where(user => user.Email.Equals(email)).FirstOrDefault();
        }

        public void Update(CardOwner user)
        {
            _context.Update(user);
        }
    }
}
