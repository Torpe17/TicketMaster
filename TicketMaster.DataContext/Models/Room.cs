namespace TicketMaster.DataContext.Models;

    public class Room
    {
        public int RoomId { get; set; }
        public String Name { get; set; }
        public int MaxSeatRow { get; set; }
        public int MaxSeatColumn { get; set; }
        public List<Screening> Screenings { get; set; }
    }