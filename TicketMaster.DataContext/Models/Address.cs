namespace TicketMaster.DataContext.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string County { get; set; }
        public int ZipCode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string? Floor { get; set; }
        public int HouseNumber { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
