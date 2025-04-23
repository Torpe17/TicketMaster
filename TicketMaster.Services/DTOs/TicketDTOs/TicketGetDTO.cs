using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketMaster.DataContext.Models;

namespace TicketMaster.Services.DTOs.TicketDTOs
{
    public class TicketGetDTO
    {
        public int Id { get; set; }
        public int ScreeningId { get; set; }
        public int? PurchaseId { get; set; }
        public int SeatRow { get; set; }
        public int SeatColumn { get; set; }
        public double Price { get; set; }
        public string FilmName { get; set; }
        public DateTime FilmDate { get; set; }
        public int RoomId { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
