namespace TicketMaster.DataContext.Models
{
    public enum Rank
    {
        NotRegistered,
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
        public DateOnly BirthDate { get; set; }
        public List<Purchase> Purchases { get; set; }
    }
}
