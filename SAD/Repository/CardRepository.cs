using Microsoft.EntityFrameworkCore;
using SAD.Model;
using System;
using System.Linq;

namespace SAD.Repository
{
    public interface ICardRepository
    {
        IQueryable<Card> GetAll();
        Card GetById(string id);
        void Add(Card entity);
        void Update(Card entity);
        void Delete(Card entity);
    }
    public class CardRepository : ICardRepository
    {
        private readonly ApplicationDbContext _context;

        public CardRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(Card entity)
        {
            _context.Add(entity);
        }

        public void Update(Card entity)
        {
            _context.Update(entity);
        }

        public void Delete(Card entity)
        {
            _context.Remove(entity);
        }

        public IQueryable<Card> GetAll()
        {
            return _context.Cards
                .Include(i => i.CardOwner)
                .Include(i => i.AllowedRooms)
                    .ThenInclude(i => i.Room);
        }

        public Card GetById(string id)
        {
            return _context.Cards.Find(id);
        }
    }
}
