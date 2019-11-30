using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAD.Model
{
    public class CardRoom
    {
        public Card Card { get; set; }
        public Guid CardId { get; set; }
        public Room Room { get; set; }
        public Guid RoomId { get; set; }
    }
}
