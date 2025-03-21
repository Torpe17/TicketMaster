namespace TicketMaster.DataContext.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int ScreeningId { get; set; }
        public int? PurchaseId { get; set; }
        public int SeatRow { get; set; }
        public int SeatColumn { get; set; }
        public double Price { get; set; }
        public Purchase Purchase { get; set; }
        public Screening Screening { get; set; }
    }
}
