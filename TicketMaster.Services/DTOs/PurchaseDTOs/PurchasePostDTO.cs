using TicketMaster.Services.DTOs.TicketDTOs;

namespace TicketMaster.Services.DTOs.PurchaseDTOs;

public class PurchasePostDTO
{
    public int? UserId { get; set; }
    public List<TicketForPurchasePostDTO> Tickets { get; set; }
}