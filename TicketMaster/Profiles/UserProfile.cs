using AutoMapper;
using TicketMaster.Services.DTOs;
using TicketMaster.DataContext.Models;
using TicketMaster.Services.DTOs.UserDTOs;
using TicketMaster.Services.DTOs.AddressDTOs;

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
                .ForMember(dest => dest.Name, opt =>
                {
                    opt.PreCondition(src => src.Username != null);
                    opt.MapFrom(src => src.Username);
                })
                .ForMember(dest => dest.Email, opt => opt.PreCondition(src => src.Email != null));
                //.ForMember(dest => dest.Roles, opt => opt.PreCondition(src => src.RoleIds != null));

            CreateMap<User, UserWithAddressDTO>()
                .ForMember(x => x.Username, opt => opt.MapFrom(x=>x.Name));

            CreateMap<RoleDTO, Role>();
            CreateMap<Role, RoleDTO>();
            CreateMap<AddressGetDTO, Address>();
            CreateMap<Address, AddressGetDTO>();
        }
    }
}
