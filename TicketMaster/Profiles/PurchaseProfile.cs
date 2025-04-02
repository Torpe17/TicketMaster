using AutoMapper;
using TicketMaster.DataContext.Models;
using TicketMaster.Services.DTOs;
using TicketMaster.Services.DTOs.PurchaseDTOs;

namespace TicketMaster.Profiles;

public class PurchaseProfile : Profile
{
    public PurchaseProfile()
    {
        CreateMap<Purchase, PurchaseGetDTO>()
            .ForMember(dest => dest.TicketCount, opt => opt.MapFrom(src => src.Tickets.Count));

        CreateMap<Purchase, PurchaseGetDTO>();
        CreateMap<Ticket, PurchaseTicketListDTO>();
        CreateMap<PurchasePutDTO, Purchase>()
            .ForMember(p => p.PurchaseDate, opt => opt.PreCondition(dto => dto.PurchaseDate != null))
            .ForMember(p => p.UserId, opt => opt.PreCondition(dto => dto.SetUserId == true))
            .ForMember(p => p.TotalPrice, opt => opt.PreCondition(dto => dto.TotalPrice != null));

    }
}