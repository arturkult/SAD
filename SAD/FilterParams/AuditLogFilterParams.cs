using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAD.FilterParams
{
    public class AuditLogFilterParams
    {
        public DateTime? Timestamp { get; set; }
        public string UserFullName { get; set; }
        public string RoomNumber { get; set; }
        public string Result { get; set; }
    }
}
