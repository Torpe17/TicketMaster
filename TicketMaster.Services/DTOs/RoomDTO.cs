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

public class RoomCreateDTO
{
    public string Name { get; set; }
    public int Capacity { get; set; }
    public int? RoomTypeId { get; set; }
}

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

public class RoomScreeningListDTO
{
    public DateTime Date { get; set; }
    public string FilmTitle { get; set; }
}