namespace TicketMaster.Services.DTOs;

public class RoomGetAllDTO
{
    public int RoomId { get; set; }
    public String Name { get; set; }
    public string RoomTypeName { get; set; }
    public int Capacity { get; set; }
    public bool DisabilityFriendly { get; set; } = false;
    public int? ComfortLevel { get; set; }
    public int ScreeningsCount { get; set; }
    public DateTime ConstructedAt { get; set; }
}