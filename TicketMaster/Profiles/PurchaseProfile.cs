using AutoMapper;
using TicketMaster.DataContext.Models;
using TicketMaster.Services.DTOs;

namespace TicketMaster.Profiles;

public class PurchaseProfile : Profile
{
    public PurchaseProfile()
    {
        CreateMap<Purchase, PurchaseGetDto>()
            .ForMember(dest => dest.TicketCount, opt => opt.MapFrom(src => src.Tickets.Count));
        
        CreateMap<PurchasePutDto, Purchase>()
            .ForMember(p => p.PurchaseDate, opt => opt.PreCondition(dto => dto.PurchaseDate != null))
            .ForMember(p => p.UserId, opt => opt.PreCondition(dto => dto.SetUserId == true))
            .ForMember(p => p.TotalPrice, opt => opt.PreCondition(dto => dto.TotalPrice != null));
    }
}