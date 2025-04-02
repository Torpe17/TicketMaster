using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketMaster.DataContext.Models;

namespace TicketMaster.Services.DTOs
{
    public class ScreeningGetDTO
    {
        public int Id { get; set; }
        public int FilmId { get; set; }
        public int RoomId { get; set; }
        public DateTime Date { get; set; }
        public string FilmName { get; set; }
        public int TicketCount { get; set; }
    }
}
