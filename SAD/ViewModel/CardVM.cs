using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAD.ViewModel
{
    public class CardVM
    {
        public Guid? Id { get; set; }
        public string SerialNumber { get; set; }
        public string CardOwnerId { get; set; }
        public string CardOwnerFullName { get; set; }
        public List<CardRoomVM> AllowedRooms { get; set; }
    }
}
