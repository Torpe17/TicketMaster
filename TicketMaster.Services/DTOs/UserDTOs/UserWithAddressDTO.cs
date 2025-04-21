using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketMaster.Services.DTOs.AddressDTOs;

namespace TicketMaster.Services.DTOs.UserDTOs
{
    public class UserWithAddressDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public IList<RoleDTO> Roles { get; set; }
        public AddressGetDTO Address { get; set; }
    }
}
