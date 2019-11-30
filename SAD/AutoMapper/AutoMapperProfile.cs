using AutoMapper;
using SAD.Model;
using SAD.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAD.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Room, RoomVM>();
            CreateMap<RoomVM, Room>();
        }
    }
}
