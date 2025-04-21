using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketMaster.DataContext.Models;
using TicketMaster.Services.DTOs;

namespace TicketMaster.Services.Profiles
{
    public class ScreeningProfile : Profile
    {
        public ScreeningProfile()
        {
            CreateMap<Screening, ScreeningGetDTO>().ForMember(
                    dest => dest.TicketCount,
                    opt => opt.MapFrom(t => t.Tickets.Count)
                ).ForMember(dest => dest.FilmName, opt => opt.MapFrom(f => f.Film.Title));

            CreateMap<ScreeningPutDTO, Screening>()
                .ForMember(s => s.FilmId, opt => opt.PreCondition(dto => dto.FilmId != null))
                .ForMember(e => e.RoomId, opt => opt.PreCondition(dto => dto.RoomId != null))
                .ForMember(e => e.Date, opt => opt.PreCondition(dto => dto.Date != null));

            CreateMap<ScreeningPostDTO, Screening>();
        }
    }
}
