namespace TicketMaster.DataContext.Models
{
    public class Purchase
    {
        [Key]
        public int Id { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
        public double TotalPrice => Tickets.Sum(t => t.Price);
        public DateTime PurchaseDate { get; set; }

    }
}
