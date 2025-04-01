using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketMaster.DataContext.Models;

namespace TicketMaster.Services.DTOs
{
    public class ScreeningPostDTO
    {
        public int FilmId { get; set; }
        public int RoomId { get; set; }
        public DateTime Date { get; set; }
        public Film Film { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}
