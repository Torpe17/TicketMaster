using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketMaster.DataContext.Models
{
    public class Purchase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
        public double TotalPrice => Tickets.Sum(t => t.Price);
        public DateTime PurchaseDate { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

    }
}
