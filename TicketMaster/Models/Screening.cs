using TicketMaster.DataContext.Entities;

namespace TicketMaster.Models
{
    public class Screening
    {
        public int Id { get; set; }
        public int FilmId { get; set; }
        public int RoomId { get; set; }
        public DateTime Date { get; set; }
        public int Seats { get; set; }
        public Film Film { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}
