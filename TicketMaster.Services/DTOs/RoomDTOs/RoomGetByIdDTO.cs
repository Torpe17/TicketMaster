namespace TicketMaster.Services.DTOs;

public class RoomGetByIdDTO
{
    public int RoomId { get; set; }
    public String Name { get; set; }
    public string? Description { get; set; }
    public string RoomTypeName { get; set; }
    public int? MaxSeatRow { get; set; }
    public int? MaxSeatColumn { get; set; }
    public int Capacity { get; set; }
    public bool DisabilityFriendly { get; set; } = false;
    public int? ComfortLevel { get; set; }
    public DateTime ConstructedAt { get; set; }
    public List<RoomScreeningListDTO> Screenings { get; set; }
}