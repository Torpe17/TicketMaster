using System.Data.Common;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketMaster.DataContext.Models;
using TicketMaster.DataContext.UnitsOfWork;
using TicketMaster.Services.DTOs;
using DbException = System.Data.Common.DbException;

namespace TicketMaster.Controllers;

[ApiController]
[Route("api/rooms")]
public class RoomsController(UnitOfWork unitOfWork, IMapper mapper, ILogger<RoomsController> logger)
    : Controller
{
    [HttpGet("getall")]
    [ProducesResponseType(typeof(IEnumerable<RoomGetAllDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<RoomGetAllDTO>>> GetRooms()
    {
        try
        {
            var rooms = await unitOfWork.RoomRepository.GetAsync(
                includedProperties: ["Screenings", "RoomType"]
            );
            if (rooms == null || !rooms.Any()) { return Ok(Enumerable.Empty<RoomGetAllDTO>()); }
            
            return Ok(mapper.Map<List<RoomGetAllDTO>>(rooms));
        }
        catch (DbException db)
        {
            logger.LogError(db, "Unexpected error occurred in the database while getting all Rooms.");
            return StatusCode(500, "A database error occured.");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Unexpected error occurred while getting all Rooms.");
            return StatusCode(500, "An unexpected error occured.");
            
        }
    }

    [HttpGet("get/{id}")]
    [ProducesResponseType(typeof(RoomGetByIdDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RoomGetByIdDTO>> GetRoom(int id)
    {
        if (id <= 0) return BadRequest("ID must be greater than zero.");
        // var room = await _unitOfWork.RoomRepository.GetByIdAsync(id, includedReferences: ["User"], includedCollections: ["Tickets"]); // race condition if both are set at the same time
        try
        {
            await unitOfWork.RoomRepository.GetByIdAsync(id, includedReferences: ["RoomType"]);
            var room = await unitOfWork.RoomRepository.GetByIdAsync(id, includedCollections: ["Screenings"]);

            if (room == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RoomGetByIdDTO>(room));
        }
        catch (DbException db)
        {
            logger.LogError(db, $"Unexpected error occurred in the database while getting room by ID: {id}.");
            return StatusCode(500, "A database error occured.");
        }
        catch (Exception e)
        {
            logger.LogError(e, $"Unexpected error occurred while getting room by ID: {id}.");
            return StatusCode(500, "An unexpected error occured.");
        }
        
    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(RoomGetByIdDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PostRoom(RoomCreateDTO room)
    {
        try
        {
            if (room.RoomTypeId != null)
            {
                RoomType? roomType = await unitOfWork.RoomTypeRepository.GetByIdAsync(room.RoomTypeId.Value);
                if (roomType == null) { return BadRequest("Room type does not exist."); }
            }
            
            Room newRoom = mapper.Map<Room>(room);

            await unitOfWork.RoomRepository.InsertAsync(newRoom);
            await unitOfWork.SaveAsync();

            return Created();
        }
        catch (DbException db)
        {
            logger.LogError(db, $"Unexpected error occurred in the database while creating Room.");
            return StatusCode(500, "A database error occured.");
        }
        catch (Exception e)
        {
            logger.LogError(e, $"Unexpected error occurred while creating Room.");
            return StatusCode(500, "An unexpected error occured.");
        }
    }

    [HttpPut("update/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutRoom(int id, RoomUpdateDTO room)
    {
        try
        {
            if (id <= 0) return BadRequest("ID must be greater than zero.");
            Room? r = await unitOfWork.RoomRepository.GetByIdAsync(id);
            if (r == null) return NotFound("Room specified by ID not found.");

            mapper.Map(room, r);
            await unitOfWork.SaveAsync();

            return NoContent();
        }
        catch (DbException db)
        {
            logger.LogError(db, $"Unexpected error occurred in the database while updating Room with ID: {id}.");
            return StatusCode(500, "A database error occured.");
        }
        catch (Exception e)
        {
            logger.LogError(e, $"Unexpected error occurred while updating Room with ID: {id}.");
            return StatusCode(500, "An unexpected error occured.");
        }
    }
    
    [HttpDelete("delete/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        if(id <= 0) return BadRequest("ID must be greater than zero.");
        try
        {
            await unitOfWork.RoomRepository.DeleteByIdAsync(id);
            await unitOfWork.SaveAsync();

            return NoContent();
        }
        catch (DbException db)
        {
            logger.LogError(db, $"Unexpected error occurred in the database while deleting Room with ID: {id}.");
            return StatusCode(500, "A database error occured.");
        }
        catch (Exception e)
        {
            logger.LogError(e, $"Unexpected error occurred while deleting Room with ID: {id}.");
            return StatusCode(500, "An unexpected error occured.");
        }
    }
}

[ApiController]
[Route("api/roomtype")]
public class RoomTypeController(UnitOfWork unitOfWork, IMapper mapper, ILogger<RoomTypeController> logger) : Controller
{
    [HttpGet("getall")]
    [ProducesResponseType(typeof(IEnumerable<RoomTypeGetDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<RoomTypeGetDTO>>> GetRoomTypes()
    {
        try
        {
            var roomTypes = await unitOfWork.RoomTypeRepository.GetAsync();
            return Ok(mapper.Map<List<RoomTypeGetDTO>>(roomTypes));
        }
        catch (DbException db)
        {
            logger.LogError(db, "Unexpected error occurred in the database while getting all Room types.");
            return StatusCode(500, "A database error occured.");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Unexpected error occurred while getting all Room types.");
            return StatusCode(500, "An unexpected error occured.");
            
        }
    }

    [HttpGet("get/{id}")]
    [ProducesResponseType(typeof(RoomType), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<RoomTypeGetDTO>>> GetRoomTypeById(int id)
    {
        if (id <= 0) return BadRequest("ID must be greater than zero.");
        try
        {
            var roomType = await unitOfWork.RoomTypeRepository.GetByIdAsync(id);

            if (roomType == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<RoomTypeGetDTO>(roomType));
        }
        catch (DbException db)
        {
            logger.LogError(db, $"Unexpected error occurred in the database while getting Room type by ID: {id}.");
            return StatusCode(500, "A database error occured.");
        }
        catch (Exception e)
        {
            logger.LogError(e, $"Unexpected error occurred while getting Room type by ID: {id}.");
            return StatusCode(500, "An unexpected error occured.");
        }
    }
    
    
    [HttpPost("create")]
    [ProducesResponseType(typeof(RoomType), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<RoomTypeCreateDTO>>> CreateRoomType(RoomTypeCreateDTO roomType)
    {
        try
        {
            RoomType newRoomType = mapper.Map<RoomType>(roomType);

            await unitOfWork.RoomTypeRepository.InsertAsync(newRoomType);
            await unitOfWork.SaveAsync();

            return Created();
        }
        catch (DbException db)
        {
            logger.LogError(db, $"Unexpected error occurred in the database while creating Room type.");
            return StatusCode(500, "A database error occured.");
        }
        catch (Exception e)
        {
            logger.LogError(e, $"Unexpected error occurred while creating Room type.");
            return StatusCode(500, "An unexpected error occured.");
        }
    }
    
    [HttpDelete("delete/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        if(id <= 0) return BadRequest("ID must be greater than zero.");
        try
        {
            await unitOfWork.RoomTypeRepository.DeleteByIdAsync(id);
            await unitOfWork.SaveAsync();

            return NoContent();
        }
        catch (DbException db)
        {
            logger.LogError(db, $"Unexpected error occurred in the database while deleting Room type with ID: {id}.");
            return StatusCode(500, "A database error occured.");
        }
        catch (Exception e)
        {
            logger.LogError(e, $"Unexpected error occurred while deleting Room type with ID: {id}.");
            return StatusCode(500, "An unexpected error occured.");
        }
    }
}