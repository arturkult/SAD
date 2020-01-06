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
            CreateMap<Room, RoomVM>()
                .ForMember(dest => dest.CardsNumber, opt => opt.MapFrom(src => src.Cards.Count()));
            CreateMap<RoomVM, Room>();

            CreateMap<CardOwner, UserVM>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id.ToString().ToLower()))
                .ForMember(dst => dst.CardsNumber, opt => opt.MapFrom(src => src.Cards.Count()));
            CreateMap<UserVM, CardOwner>()
                .ForSourceMember(src => src.CardsNumber, opt => opt.DoNotValidate());

            CreateMap<AuditLog, AuditLogVM>();

            CreateMap<Card, CardVM>();
            CreateMap<CardVM, Card>()
                .ForMember(dest => dest.CardOwner, opt => opt.MapFrom(src => new CardOwner
                {
                    Id = Guid.Parse(src.CardOwnerId)
                }));

            CreateMap<CardRoom, CardRoomVM>();
            CreateMap<CardRoomVM, CardRoom>();

        }
    }
}
