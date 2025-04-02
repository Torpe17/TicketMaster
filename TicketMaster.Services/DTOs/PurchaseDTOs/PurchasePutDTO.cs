namespace TicketMaster.Services.DTOs.PurchaseDTOs;

public class PurchasePutDTO
{
    public bool SetUserId { get; set; }
    public int? UserId { get; set; }
    public DateTime? PurchaseDate { get; set; }
    public double? TotalPrice { get; set; }
}