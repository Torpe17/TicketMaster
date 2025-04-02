namespace TicketMaster.Services.DTOs;

public class PurchaseGetDto
{
    public int Id { get; set; }
    public int? UserId { get; set; }
    public string? UserName { get; set; }
    public double TotalPrice { get; set; }
    public int? TicketCount { get; set; }
    public DateTime PurchaseDate { get; set; }
}