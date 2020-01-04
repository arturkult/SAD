using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAD.ViewModel
{
    public class AuditLogVM
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string UserFullName { get; set; }
        public string RoomNumber { get; set; }
        public bool? Result { get; set; }
    }
}
