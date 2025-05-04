namespace TicketMaster.DataContext.Models
{
    public class Screening
    {
        public int Id { get; set; }
        public int FilmId { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public DateTime Date { get; set; }
        public Film Film { get; set; }
        public List<Ticket> Tickets { get; set; }
        public double DefaultTicketPrice { get; set; }
    }
}
