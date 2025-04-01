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

            CreateMap<AddressPutDTO, Address>()
                .ForMember(a => a.Country, opt => opt.PreCondition(dto => dto.Country != null))
                .ForMember(a => a.County, opt => opt.PreCondition(dto => dto.County != null))
                .ForMember(a => a.ZipCode, opt => opt.PreCondition(dto => dto.ZipCode != null))
                .ForMember(a => a.City, opt => opt.PreCondition(dto => dto.City != null))
                .ForMember(a => a.Street, opt => opt.PreCondition(dto => dto.Street != null))
                .ForMember(a => a.HouseNumber, opt => opt.PreCondition(dto => dto.HouseNumber != null))
                .ForMember(a => a.UserId, opt => opt.PreCondition(dto => dto.UserId != null))
                .ForMember(a => a.Floor, opt => opt.PreCondition(dto => dto.SetFloor == true));
        }
    }
}
