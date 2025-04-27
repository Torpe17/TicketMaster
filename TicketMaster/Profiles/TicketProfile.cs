using AutoMapper;
using TicketMaster.DataContext.Models;
using TicketMaster.Services.DTOs.TicketDTOs;

namespace TicketMaster.Profiles
{
    public class TicketProfile : Profile
    {
        public TicketProfile()
        {
            CreateMap<Ticket, TicketGetDTO>()
                .ForMember(dest => dest.FilmName, opt => opt.MapFrom(t => t.Screening.Film.Title))
                .ForMember(dest => dest.FilmDate, opt => opt.MapFrom(t => t.Screening.Date))
                .ForMember(dest => dest.RoomId, opt => opt.MapFrom(t => t.Screening.RoomId))
                .ForMember(dest => dest.PurchaseDate, opt =>
                {
                    opt.PreCondition(src => src.PurchaseId != null);
                    opt.MapFrom(src => src.Purchase.PurchaseDate);
                });

            CreateMap<TicketPutDTO, Ticket>()
                .ForMember(e => e.ScreeningId, opt => opt.PreCondition(dto => dto.ScreeningId != null))
                .ForMember(e => e.SeatRow, opt => opt.PreCondition(dto => dto.SeatRow != null))
                .ForMember(e => e.SeatColumn, opt => opt.PreCondition(dto => dto.SeatColumn != null))
                .ForMember(e => e.PurchaseId, opt => opt.PreCondition(dto => dto.SetPurchaseId == true));

            CreateMap<TicketPostDTO, Ticket>();

            CreateMap<TicketForPurchasePostDTO, Ticket>();
        }
    }
}
