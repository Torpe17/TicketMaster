using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TicketMaster.DataContext.Models;
using TicketMaster.Services.DTOs;

namespace TicketMaster.Services.Profiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressGetDTO>();
        }
    }
}
