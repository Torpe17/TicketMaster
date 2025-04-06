using AutoMapper;
using TicketMaster.DataContext.Models;
using TicketMaster.Services.DTOs;
using TicketMaster.Services.DTOs.PurchaseDTOs;

namespace TicketMaster.Profiles;

public class RoomProfile : Profile
{
    public RoomProfile()
    {
        // get
        CreateMap<Room, RoomGetAllDTO>()
            .ForMember(dest => dest.ScreeningsCount, opt => opt.MapFrom(src => src.Screenings.Count));

        // getbyid
        CreateMap<Room, RoomGetByIdDTO>();
        CreateMap<Screening, RoomScreeningListDTO>();

        // post
        CreateMap<RoomCreateDTO, Room>();

        // put
        CreateMap<RoomUpdateDTO, Room>()
            .ForMember(r => r.Name, opt => opt.PreCondition(dto => dto.Name != null))
            .ForMember(r => r.Description, opt => opt.PreCondition(dto => dto.Description != null))
            .ForMember(r => r.RoomTypeId, opt => opt.PreCondition(dto => dto.SetRoomType == true))
            .ForMember(r => r.MaxSeatRow, opt => opt.PreCondition(dto => dto.MaxSeatRow != null))
            .ForMember(r => r.MaxSeatColumn, opt => opt.PreCondition(dto => dto.MaxSeatColumn != null))
            .ForMember(r => r.Capacity, opt => opt.PreCondition(dto => dto.Capacity != null))
            .ForMember(r => r.DisabilityFriendly, opt => opt.PreCondition(dto => dto.DisabilityFriendly != null))
            .ForMember(r => r.ComfortLevel, opt => opt.PreCondition(dto => dto.ComfortLevel != null))
            .ForMember(r => r.ConstructedAt, opt => opt.PreCondition(dto => dto.ConstructedAt != null));
    }
}

public class RoomTypeProfile : Profile
{
    public RoomTypeProfile()
    {
        // get
        CreateMap<RoomType, RoomTypeGetDTO>();
        
        // getbyid
        CreateMap<RoomType, RoomTypeGetDTO>();

        // post
        CreateMap<RoomTypeCreateDTO, RoomType>();
    }
}