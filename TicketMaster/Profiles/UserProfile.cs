using AutoMapper;
using TicketMaster.Services.DTOs;
using TicketMaster.DataContext.Models;
using TicketMaster.Services.DTOs.UserDTOs;

namespace TicketMaster.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRegisterDTO, User>();
            CreateMap<User, UserDTO>()
                .ForMember(x => x.Username, opt => opt.MapFrom(x=>x.Name));
            CreateMap<UserUpdateDTO, User>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Username));
            CreateMap<User, UserWithAddressDTO>()
                .ForMember(x => x.Username, opt => opt.MapFrom(x=>x.Name));

            CreateMap<RoleDTO, Role>();
            CreateMap<Role, RoleDTO>();
            CreateMap<AddressGetDTO, Address>();
            CreateMap<Address, AddressGetDTO>();
        }
    }
}
