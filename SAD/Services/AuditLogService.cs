using SAD.FilterParams;
using SAD.Model;
using SAD.Repository;
using SAD.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAD.Services
{
    public interface IAuditLogService
    {
        AuditLog Add(string CardSerialNumber, string RoomNumber, bool result);
        IQueryable<AuditLog> GetAll();
        IQueryable<AuditLog> GetAll(AuditLogFilterParams queryParams);
    }
    public class AuditLogService : IAuditLogService
    {
        private readonly IAuditLogRepository _auditLogRepository;
        private readonly ApplicationDbContext _context;

        public AuditLogService(IAuditLogRepository auditLogRepository, ApplicationDbContext context)
        {
            _auditLogRepository = auditLogRepository;
            _context = context;
        }
        public AuditLog Add(string cardSerialNumber, string roomNumber, bool result)
        {
            var entity = new AuditLog()
            {
                User = _context.CardOwners.FirstOrDefault(owner => owner.Cards.Any(card => card.SerialNumber.Equals(cardSerialNumber))),
                Room = _context.Rooms.FirstOrDefault(room => room.Number.Equals(roomNumber)),
                Timestamp = DateTime.Now,
                Result = result
            };
            _auditLogRepository.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public IQueryable<AuditLog> GetAll()
        {
            return _auditLogRepository
                .GetAll()
                .OrderByDescending(log => log.Timestamp);
        }

        public IQueryable<AuditLog> GetAll(AuditLogFilterParams queryParams)
        {

            var query = GetAll();
            if (bool.TryParse(queryParams.Result, out var result))
            {
                query = query.Where(log => log.Result == result);
            }
            if (queryParams.RoomNumber != null)
            {
                query = query.Where(log => log.Room.Number.Contains(queryParams.RoomNumber));
            }
            if (queryParams.UserFullName != null)
            {
                query = query.Where(log => log.User.FullName.Contains(queryParams.UserFullName));
            }
            if (queryParams.Timestamp.HasValue)
            {
                query = query.Where(log => log.Timestamp.Date >= queryParams.Timestamp.Value.Date);
            }
            return query;
        }
    }
}
