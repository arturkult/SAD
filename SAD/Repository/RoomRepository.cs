using SAD.Model;
using System;
using System.Linq;

namespace SAD.Repository
{
    public interface IRoomRepository
    {
        IQueryable<Room> GetAll();
        Room GetById(string id);
        void Add(Room entity);
        void Update(Room entity);
        void Delete(Room entity);
    }
    public class RoomRepository : IRoomRepository
    {
        private readonly ApplicationDbContext _context;

        public RoomRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(Room entity)
        {
            _context.Add(entity);
        }

        public void Update(Room entity)
        {
            _context.Update(entity);
        }

        public void Delete(Room entity)
        {
            _context.Remove(entity);
        }

        public IQueryable<Room> GetAll()
        {
            return _context.Rooms;
        }

        public Room GetById(string id)
        {
            return _context.Rooms.Find(id);
        }
    }
}
