namespace TicketMaster.Services.DTOs.PurchaseDTOs;

public class PurchaseGetByIdDTO
{
    public int Id { get; set; }
    public int? UserId { get; set; }
    public string? UserName { get; set; }
    public double TotalPrice { get; set; }
    public DateTime PurchaseDate { get; set; }
    public List<PurchaseTicketListDTO> Tickets { get; set; }
}