using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAD.Model
{
    public class Card
    {
        public Guid Id { get; set; }
        public string SerialNumber { get; set; }
        public CardOwner CardOwner { get; set; }
        public virtual List<CardRoom> AllowedRooms { get; set; }
    }
}
