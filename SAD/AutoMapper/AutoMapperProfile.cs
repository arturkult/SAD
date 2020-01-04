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
            CreateMap<CardOwner, UserVM>()
                .ForMember(dst => dst.CardsNumber, opt => opt.MapFrom(src => 0));
            CreateMap<UserVM, CardOwner>()
                .ForSourceMember(src => src.CardsNumber, opt => opt.DoNotValidate());
            CreateMap<AuditLog, AuditLogVM>();
        }
    }
}
