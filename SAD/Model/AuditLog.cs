using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAD.Model
{
    public class AuditLog
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public CardOwner User { get; set; }
        public Room Room { get; set; }
        public bool Result { get; set; }
    }
}
