using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketMaster.DataContext.Models;

namespace TicketMaster.Services.DTOs.ScreeningDTOs
{
    public class ScreeningPutDTO
    {
        public int? FilmId { get; set; }
        public int? RoomId { get; set; }
        public DateTime? Date { get; set; }
        public double? DefaultTicketPrice { get; set; }
    }
}
