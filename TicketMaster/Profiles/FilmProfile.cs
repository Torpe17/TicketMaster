using AutoMapper;
using TicketMaster.DataContext.Models;
using TicketMaster.Services.DTOs;

namespace TicketMaster.Profiles
{
    public class FilmProfile : AutoMapper.Profile
    {
        public FilmProfile()
        {
            CreateMap<Film, FilmGetDTO>();

          //  CreateMap<Screening, ScreeningGetDTO>()
               // .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Screening.Id));
               // .ForMember(dest => dest.FilmId, opt => opt.MapFrom(src => src.Screening.FilmId));
               // .ForMember(dest => dest.RoomId, opt => opt.MapFrom(src => src.Screening.RoomId));
               // .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Screening.Date));
               // .ForMember(dest => dest.TicketCount, opt => opt.MapFrom(src => src.Screening.Tickets.Count));

            CreateMap<FilmPostDTO, Film>();

            CreateMap<FilmPutDTO, Film>()
                .ForMember(e => e.Title, opt => opt.PreCondition(dto => dto.Title != null))
                .ForMember(e => e.Director, opt => opt.PreCondition(dto => dto.Director != null))
                .ForMember(e => e.Genre, opt => opt.PreCondition(dto => dto.Genre != null))
                .ForMember(e => e.Length, opt => opt.PreCondition(dto => dto.Length != null))
                .ForMember(e => e.Description, opt => opt.PreCondition(dto => dto.Description != null))
                .ForMember(e => e.AgeRating, opt => opt.PreCondition(dto => dto.SetAgeRating == true));
        }
    }
}
