using Microsoft.EntityFrameworkCore;
using SAD.Model;
using SAD.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAD.Services
{
    public interface ICardService
    {
        void Add(Card card);
        void Update(Card card);

        void Block(string id);
        IQueryable<Card> GetAll();
    }
    public class CardService : ICardService
    {
        private readonly ICardRepository _repository;
        private readonly ApplicationDbContext _context;

        public CardService(ICardRepository repository, ApplicationDbContext context)
        {
            _repository = repository;
            _context = context;
        }
        public void Add(Card card)
        {
            card.CardOwner = _context.CardOwners.Find(card.CardOwner.Id);
            SyncRooms(card);
            _repository.Add(card);
            _context.SaveChanges();
        }

        public void Block(string id)
        {
            var guidId = Guid.Parse(id);
            _context.CardRoom.RemoveRange(_context.CardRoom.Where(cr => cr.CardId.Equals(id)));
            _context.SaveChanges();
        }

        public IQueryable<Card> GetAll()
        {
            return _repository.GetAll();
        }

        public void Update(Card card)
        {
            card.CardOwner = _context.CardOwners.Find(card.CardOwner.Id);
            var cardRoomsFromDB = _context.CardRoom.Where(cr => cr.CardId.Equals(card.Id));
            var cardsToRemove = cardRoomsFromDB.Where(cardRoom => !card.AllowedRooms.Any(cr => cr.RoomId.Equals(cardRoom.RoomId)));
            if (cardsToRemove.Any())
            {
                _context.CardRoom.RemoveRange(cardsToRemove);
            }
            //_context.SaveChanges();
            SyncRooms(card);
            _repository.Update(card);
            _context.SaveChanges();
        }

        private void SyncRooms(Card card)
        {
            card.AllowedRooms.ForEach(cardRoom =>
            {
                cardRoom.Room = _context.Rooms.Find(cardRoom.RoomId);
            });
        }
    }
}
