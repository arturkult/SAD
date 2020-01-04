using Microsoft.EntityFrameworkCore;
using SAD.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAD.Repository
{
    public interface IAuditLogRepository
    {
        IQueryable<AuditLog> GetAll();
        void Add(AuditLog entity);
    }
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly ApplicationDbContext _context;

        public AuditLogRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(AuditLog entity)
        {
            _context.Add(entity);
        }

        public IQueryable<AuditLog> GetAll()
        {
            return _context.AuditLogs
                .Include(i=> i.User)
                .Include(i=> i.Room);
        }
    }
}
