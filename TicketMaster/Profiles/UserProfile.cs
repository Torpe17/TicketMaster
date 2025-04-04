using AutoMapper;
using TicketMaster.Services.DTOs;
using TicketMaster.DataContext.Models;

namespace TicketMaster.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRegisterDTO, User>();
            CreateMap<User, UserDTO>()
                .ForMember(x => x.Username, opt => opt.MapFrom(x=>x.Name));
        }
    }
}
