namespace TicketMaster.Services.DTOs.PurchaseDTOs;

public class PurchaseGetDTO
{
    public int Id { get; set; }
    public int? UserId { get; set; }
    public string? UserName { get; set; }
    public double TotalPrice { get; set; }
    public int? TicketCount { get; set; }
    public string TicketFilmName { get; set; }
    public DateTime PurchaseDate { get; set; }
}