using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketMaster.DataContext.Models;

namespace TicketMaster.Services.DTOs
{
    public class AddressGetDTO
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string County { get; set; }
        public int ZipCode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string? Floor { get; set; }
        public int HouseNumber { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
