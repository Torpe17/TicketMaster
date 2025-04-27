using System.Collections;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TicketMaster.DataContext.Context;
using TicketMaster.DataContext.Models;
using TicketMaster.DataContext.UnitsOfWork;
using TicketMaster.Services.DTOs;

namespace TicketMaster.Services;

public interface IRoomService
{
    // Rooms
    Task<IEnumerable<RoomGetAllDTO>> GetAllRoomsAsync();
    Task<IEnumerable<RoomGetAllDTO>> GetEmptyRoomsAsync();
    Task<RoomGetByIdDTO> GetRoomByIdAsync(int id);
    Task AddRoomAsync(RoomCreateDTO dto);
    Task UpdateRoomAsync(int id, RoomUpdateDTO dto);
    Task DeleteRoomAsync(int id);
    
    // Room Types
    Task<IEnumerable<RoomTypeGetDTO>> GetRoomTypesAsync();
    Task CreateRoomTypeAsync(RoomTypeCreateDTO dto);
    Task DeleteRoomTypeAsync(int id);
    
}

public class RoomService(UnitOfWork unitOfWork, IMapper mapper, AppDbContext dbContext) : IRoomService
{
    // ROOMS
    public async Task<IEnumerable<RoomGetAllDTO>> GetAllRoomsAsync()
    {
        return mapper.Map<List<RoomGetAllDTO>>(await unitOfWork.RoomRepository.GetAsync(includedProperties: ["RoomType", "Screenings"]));
    }

    public async Task<IEnumerable<RoomGetAllDTO>> GetEmptyRoomsAsync()
    {
        var currentDate = DateTime.Now;
    
        var emptyRooms = await dbContext.Rooms.Include(x => x.RoomType).Include(x => x.Screenings)
            .Where(r => !r.Screenings.Any(s => s.Date >= currentDate)) 
            .ToListAsync();

        return mapper.Map<List<RoomGetAllDTO>>(emptyRooms);
    }

    public async Task<RoomGetByIdDTO> GetRoomByIdAsync(int id)
    {
        if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));
    
        var room = await dbContext.Rooms
            .Include(r => r.RoomType)
            .Include(r => r.Screenings)
            .ThenInclude(s => s.Film) // Explicit ThenInclude
            .FirstOrDefaultAsync(r => r.RoomId == id);

        if (room == null) throw new KeyNotFoundException();
    
        return mapper.Map<RoomGetByIdDTO>(room);
    }

    public async Task AddRoomAsync(RoomCreateDTO dto)
    {
        var room = mapper.Map<Room>(dto);
        room.ConstructedAt = DateTime.UtcNow;
        await unitOfWork.RoomRepository.InsertAsync(room);
        await unitOfWork.SaveAsync();
    }

    public async Task UpdateRoomAsync(int id, RoomUpdateDTO dto)
    {
        if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));
        var existingRoom = await unitOfWork.RoomRepository.GetByIdAsync(id);
        if (existingRoom == null) throw new KeyNotFoundException();

        mapper.Map(dto, existingRoom);
        await unitOfWork.SaveAsync();
    }

    public async Task DeleteRoomAsync(int id)
    {
        if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));
        var roomToBeDeleted = await unitOfWork.RoomRepository.GetByIdAsync(id);
        if (roomToBeDeleted == null) throw new KeyNotFoundException();

        await unitOfWork.RoomRepository.DeleteByIdAsync(id);
        await unitOfWork.SaveAsync();
    }

    
    // ROOM TYPES
    public async Task<IEnumerable<RoomTypeGetDTO>> GetRoomTypesAsync()
    {
        return mapper.Map<List<RoomTypeGetDTO>>(await unitOfWork.RoomTypeRepository.GetAsync());
    }

    public async Task CreateRoomTypeAsync(RoomTypeCreateDTO dto)
    {
        await unitOfWork.RoomTypeRepository.InsertAsync(mapper.Map<RoomType>(dto));
        await unitOfWork.SaveAsync();
    }

    public async Task DeleteRoomTypeAsync(int id)
    {
        if (id <= 0) throw new ArgumentOutOfRangeException(nameof(id));
        var roomTypeToBeDeleted = await unitOfWork.RoomTypeRepository.GetByIdAsync(id);
        if (roomTypeToBeDeleted == null) throw new KeyNotFoundException();

        await unitOfWork.RoomTypeRepository.DeleteByIdAsync(id);
        await unitOfWork.SaveAsync();
    }
}