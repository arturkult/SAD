using Microsoft.EntityFrameworkCore;
using SAD.Model;
using SAD.Repository;
using SAD.ViewModel;
using System;
using System.Linq;

namespace SAD.Services
{
    public interface IRoomService
    {
        IQueryable<Room> GetAll();
        Room GetById(string id);
        Guid Add(Room entity);
        Guid Update(Room entity);
        void Delete(Room entity);
        bool CheckAccess(RequestVM request);
    }
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly ICardRepository _cardRepository;
        private readonly ApplicationDbContext _context;

        public RoomService(IRoomRepository roomRepository,
            ICardRepository cardRepository,
            ApplicationDbContext context)
        {
            _roomRepository = roomRepository;
            _cardRepository = cardRepository;
            _context = context;
        }

        public Guid Add(Room entity)
        {
            _roomRepository.Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public bool CheckAccess(RequestVM request)
        {
            var cardFromDb = _cardRepository
                .GetAll()
                .Include(i => i.AllowedRooms)
                    .ThenInclude(i => i.Room)
                .FirstOrDefault(card => card.SerialNumber.Equals(request.CardSerialNumber));
            return cardFromDb?
                .AllowedRooms?
                .Any(cardRoom => cardRoom
                    .Room
                    .Number
                    .Equals(request.RoomNumber))
                ?? false;
        }

        public void Delete(Room entity)
        {
            _roomRepository.Delete(entity);
            _context.SaveChanges();
        }

        public IQueryable<Room> GetAll()
        {
            return _roomRepository.GetAll();
        }

        public Room GetById(string id)
        {
            return _roomRepository.GetById(id);
        }

        public Guid Update(Room entity)
        {
            _roomRepository.Update(entity);
            _context.SaveChanges();
            return entity.Id;
        }

    }
}
