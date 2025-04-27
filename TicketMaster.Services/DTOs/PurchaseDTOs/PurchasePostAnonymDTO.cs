using System.ComponentModel.DataAnnotations;
using TicketMaster.Services.DTOs.TicketDTOs;

namespace TicketMaster.Services.DTOs.PurchaseDTOs;

public class PurchasePostAnonymDTO
{
    [EmailAddress]
    public string Email { get; set; }
    [Phone]
    public string PhoneNumber { get; set; }
    public List<TicketForPurchasePostDTO> Tickets { get; set; }
}