using AutoMapper;
using TicketMaster.DataContext.Models;
using TicketMaster.Services.DTOs;
using TicketMaster.Services.DTOs.PurchaseDTOs;

namespace TicketMaster.Profiles;

public class PurchaseProfile : Profile
{
    public PurchaseProfile()
    {
        // get
        CreateMap<Purchase, PurchaseGetDTO>()
            .ForMember(dest => dest.TicketCount, opt => opt.MapFrom(src => src.Tickets.Count))
            .ForMember(dest => dest.TicketFilmName, opt => opt.MapFrom(src => src.Tickets.FirstOrDefault().Screening.Film.Title));

        // getbyid
        CreateMap<Purchase, PurchaseGetByIdDTO>();
        CreateMap<Ticket, PurchaseTicketListDTO>();
        
        // post
        CreateMap<PurchasePostDTO, Purchase>();
    }
}