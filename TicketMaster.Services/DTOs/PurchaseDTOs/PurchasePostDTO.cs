using TicketMaster.Services.DTOs.TicketDTOs;

namespace TicketMaster.Services.DTOs.PurchaseDTOs;

public class PurchasePostDTO
{
    public int? UserId { get; set; }
    public List<TicketPostDTO> Tickets { get; set; }
}