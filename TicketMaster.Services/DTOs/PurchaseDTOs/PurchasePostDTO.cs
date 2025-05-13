using System.ComponentModel.DataAnnotations;
using TicketMaster.Services.DTOs.TicketDTOs;

namespace TicketMaster.Services.DTOs.PurchaseDTOs;

public class PurchasePostDTO
{
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public List<TicketForPurchasePostDTO> Tickets { get; set; }
}