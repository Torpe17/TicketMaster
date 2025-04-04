using AutoMapper;
using TicketMaster.DataContext.Models;
using TicketMaster.Services.DTOs;

namespace TicketMaster.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleDTO>();
        }
    }
}
