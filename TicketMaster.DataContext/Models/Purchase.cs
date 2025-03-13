namespace TicketMaster.DataContext.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public double TotalPrice { get; set; }
        public User User { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}
