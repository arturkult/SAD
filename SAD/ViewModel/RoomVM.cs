using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAD.ViewModel
{
    public class RoomVM
    {
        public Guid? Id { get; set; }
        public string Number { get; set; }
        public int Floor { get; set; }
        public int CardsNumber { get; set; }
    }
}
