using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketMaster.Services.DTOs.TicketDTOs
{
    public class TicketForPurchasePostDTO
    {
        public int ScreeningId { get; set; }
        public int? SeatRow { get; set; }
        public int? SeatColumn { get; set; }
    }
}
