namespace TicketMaster.Services.DTOs;

public class RoomTypeGetDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class RoomTypeCreateDTO
{
    public string Name { get; set; }
}