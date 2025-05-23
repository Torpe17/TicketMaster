namespace TicketMaster.Services.DTOs;

public class RoomCreateDTO
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public int? MaxSeatRow { get; set; }
    public int? MaxSeatColumn { get; set; }
    public int Capacity { get; set; }
    public int? RoomTypeId { get; set; }
    public bool? DisabilityFriendly  { get; set; }
    public int? ComfortLevel { get; set; }
}