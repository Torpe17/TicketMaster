namespace TicketMaster.Services.DTOs.PurchaseDTOs;

public class PurchaseGetDTO
{
    public int Id { get; set; }
    public int? UserId { get; set; }
    public string? UserName { get; set; }
    public double TotalPrice => Tickets.Sum(ticket => ticket.Price);
    public int? TicketCount { get; set; }
    public string TicketFilmName { get; set; }
    public DateTime ScreeningTime { get; set; }
    public List<PurchaseTicketListDTO> Tickets { get; set; }
    public DateTime PurchaseDate { get; set; }
}