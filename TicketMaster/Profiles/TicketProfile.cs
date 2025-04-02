using AutoMapper;
using TicketMaster.DataContext.Models;
using TicketMaster.Services.DTOs;

namespace TicketMaster.Profiles
{
    public class TicketProfile : Profile
    {
        public TicketProfile()
        {
            CreateMap<Ticket, TicketGetDTO>();

            CreateMap<TicketPutDTO, Ticket>()
                .ForMember(e => e.ScreeningId, opt => opt.PreCondition(dto => dto.ScreeningId != null))
                .ForMember(e => e.SeatRow, opt => opt.PreCondition(dto => dto.SeatRow != null))
                .ForMember(e => e.SeatColumn, opt => opt.PreCondition(dto => dto.SeatColumn != null))
                .ForMember(e => e.PurchaseId, opt => opt.PreCondition(dto => dto.SetPurchaseId == true));

            CreateMap<TicketPostDTO, Ticket>();
        }
    }
}
