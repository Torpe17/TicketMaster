namespace TicketMaster.Models
{
    public enum Rank
    {
        Not_registered,
        Registered,
        Cashier,
        Admin
    }
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Rank Rank { get; set; }
        public Address Address { get; set; }
        public List<Purchase> Pruchases { get; set; }
    }
}
