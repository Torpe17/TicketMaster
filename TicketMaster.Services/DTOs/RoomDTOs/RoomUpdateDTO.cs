namespace TicketMaster.Services.DTOs;

public class RoomUpdateDTO
{
    public String? Name { get; set; }
    public string? Description { get; set; }
    public bool SetRoomType { get; set; }
    public int? RoomTypeId { get; set; }
    public int? MaxSeatRow { get; set; }
    public int? MaxSeatColumn { get; set; }
    public int? Capacity { get; set; }
    public bool? DisabilityFriendly { get; set; }
    public int? ComfortLevel { get; set; }
    public DateTime? ConstructedAt { get; set; }
}