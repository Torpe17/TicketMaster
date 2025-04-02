namespace TicketMaster.Services.DTOs.PurchaseDTOs;

public class PurchasePostDTO
{
    public int Id { get; set; }
    public int? UserId { get; set; }
    public DateTime PurchaseDate { get; set; }
    public double TotalPrice { get; set; }
}