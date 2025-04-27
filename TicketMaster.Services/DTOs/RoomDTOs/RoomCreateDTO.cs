namespace TicketMaster.Services.DTOs;

public class RoomCreateDTO
{
    public string Name { get; set; }
    public int Capacity { get; set; }
    public int? RoomTypeId { get; set; }
}