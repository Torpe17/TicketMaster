namespace TicketMaster.Services.DTOs.PurchaseDTOs;

public class PurchaseTicketListDTO
{
    public int Id { get; set; }
    public int PurchaseId { get; set; }
    public int SeatRow { get; set; }
    public int SeatColumn { get; set; }
    public double Price { get; set; }
}