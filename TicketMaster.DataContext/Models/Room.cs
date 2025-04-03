namespace TicketMaster.DataContext.Models;

    public class Room
    {
        public int RoomId { get; set; }
        public String Name { get; set; }
        public string? Description { get; set; }
        public RoomType Type { get; set; } = RoomType.Other;
        public int? MaxSeatRow { get; set; }
        public int? MaxSeatColumn { get; set; }
        public int Capacity { get; set; }
        public bool DisabilityFriendly { get; set; } = false;
        public int? ComfortLevel { get; set; }
        public List<Screening> Screenings { get; set; }

        public enum RoomType
        {
            Movie,
            Theater,
            Concert,
            Other
        }
    }