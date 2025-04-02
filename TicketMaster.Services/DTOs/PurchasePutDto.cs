using TicketMaster.DataContext.Models;

namespace TicketMaster.Services.DTOs;

public class PurchasePutDto
{
    public bool SetUserId { get; set; }
    public int? UserId { get; set; }
    public DateTime? PurchaseDate { get; set; }
    public double? TotalPrice { get; set; }
}