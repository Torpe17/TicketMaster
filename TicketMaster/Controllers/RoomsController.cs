using System.Data.Common;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketMaster.DataContext.Models;
using TicketMaster.DataContext.UnitsOfWork;
using TicketMaster.Services;
using TicketMaster.Services.DTOs;
using ArgumentOutOfRangeException = System.ArgumentOutOfRangeException;
using DbException = System.Data.Common.DbException;

namespace TicketMaster.Controllers;

[ApiController]
[Route("api")]
[Authorize(Roles = "Admin")]
public class RoomsController(IRoomService roomService, ILogger<RoomsController> logger)
    : Controller
{
    /* ROOM ENDPOINTS */
    // api/rooms
    [HttpGet("rooms")]
    [ProducesResponseType(typeof(IEnumerable<RoomGetAllDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<RoomGetAllDTO>>> GetRooms()
    {
        try
        {
            return Ok(await roomService.GetAllRoomsAsync());
        }
        catch (DbException db)
        {
            logger.LogError(db, $"DatabaseException occurred while getting all rooms.\nERROR MESSAGE: {db.Message}\n INNER MESSAGE: {db.InnerException?.Message}");
            return StatusCode(500, "A database error occured.");
        }
        catch (Exception e)
        {
            logger.LogError(e, $"Unexpected error occurred while getting all rooms.\nERROR MESSAGE: {e.Message}");
            return StatusCode(500, "An unexpected error occured.");
            
        }
    }
    
    // api/emptyrooms
    [HttpGet("emptyrooms")]
    [ProducesResponseType(typeof(IEnumerable<RoomGetAllDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<RoomGetAllDTO>>> GetEmptyRooms()
    {
        try
        {
            return Ok(await roomService.GetEmptyRoomsAsync());
        }
        catch (DbException db)
        {
            logger.LogError(db, $"DatabaseException occurred while getting all empty rooms.\nERROR MESSAGE: {db.Message}\n INNER MESSAGE: {db.InnerException?.Message}");
            return StatusCode(500, "A database error occured.");
        }
        catch (Exception e)
        {
            logger.LogError(e, $"Unexpected error occurred while getting all empty rooms.\nERROR MESSAGE: {e.Message}");
            return StatusCode(500, "An unexpected error occured.");
            
        }
    }
    
    // api/room/5
    [HttpGet("room/{roomId:int}")]
    [ProducesResponseType(typeof(RoomGetByIdDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RoomGetByIdDTO>> GetRoomByRoomId(int roomId)
    {
        try
        {
            return Ok(await roomService.GetRoomByIdAsync(roomId));
        }
        catch (DbException db)
        {
            logger.LogError(db,
                $"DatabaseException occurred while getting room with roomId: {roomId}.\nERROR MESSAGE: {db.Message}\n INNER MESSAGE: {db.InnerException?.Message}");
            return StatusCode(500, "A database error occured.");
        }
        catch (ArgumentOutOfRangeException aoorEx)
        {
            logger.LogError(aoorEx,
                $"ArgumentOutOfRangeException occured while getting room with roomId: {roomId}.\nERROR MESSAGE: {aoorEx.Message}");
            return StatusCode(400, "RoomId is out of range.");
        }
        catch (KeyNotFoundException knfEx)
        {
            logger.LogError(knfEx, $"KeyNotFoundException occured while getting room with roomId: {roomId}.\nERROR MESSAGE: {knfEx.Message}");
            return StatusCode(404, "Room not found.");
        }
        catch (Exception e)
        {
            logger.LogError(e, $"Unexpected error occurred while getting room with roomId: {roomId}.\nERROR MESSAGE: {e.Message}");
            return StatusCode(500, "An unexpected error occured.");
        }
        
    }

    // api/room
    [HttpPost("room")]
    [ProducesResponseType(typeof(RoomGetByIdDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PostRoom(RoomCreateDTO room)
    {
        try
        {
            await roomService.AddRoomAsync(room);
            return Created();
        }
        catch (DbException db)
        {
            logger.LogError(db, $"DatabaseException occurred while creating room.\nERROR MESSAGE: {db.Message}\n INNER MESSAGE: {db.InnerException?.Message}");
            return StatusCode(500, "A database error occured.");
        }
        catch (Exception e)
        {
            logger.LogError(e, $"Unexpected error occurred while creating room.\nERROR MESSAGE: {e.Message}");
            return StatusCode(500, "An unexpected error occured.");
        }
    }

    // api/room/5
    [HttpPut("room/{roomId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PutRoom(int roomId, RoomUpdateDTO room)
    {
        try
        {
            await roomService.UpdateRoomAsync(roomId, room);
            return NoContent();
        }
        catch (DbException db)
        {
            logger.LogError(db,
                $"DatabaseException occurred while updating Room with roomId: {roomId}.\nERROR MESSAGE: {db.Message}\n INNER MESSAGE: {db.InnerException?.Message}");
            return StatusCode(500, "A database error occured.");
        }
        catch (ArgumentOutOfRangeException aoorEx)
        {
            logger.LogError(aoorEx,
                $"ArgumentOutOfRangeException occured while updating room with roomId: {roomId}.\nERROR MESSAGE: {aoorEx.Message}");
            return StatusCode(400, "RoomId is out of range.");
        }
        catch (KeyNotFoundException knfEx)
        {
            logger.LogError(knfEx, $"KeyNotFoundException occured while updating room with roomId: {roomId}.\nERROR MESSAGE: {knfEx.Message}");
            return StatusCode(404, $"Room not found with roomId: {roomId}.");
        }
        catch (Exception e)
        {
            logger.LogError(e, $"Unexpected error occurred while updating room with roomId: {roomId}.\nERROR MESSAGE: {e.Message}");
            return StatusCode(500, "An unexpected error occured.");
        }
    }
    
    // api/room/5
    [HttpDelete("room/{roomId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteRoom(int roomId)
    {
        try
        {
            await roomService.DeleteRoomAsync(roomId);
            return NoContent();
        }
        catch (DbException db)
        {
            logger.LogError(db,
                $"DatabaseException occurred while deleting room with roomId: {roomId}.\nERROR MESSAGE: {db.Message}\n INNER MESSAGE: {db.InnerException?.Message}");
            return StatusCode(500, "A database error occured.");
        }
        catch (ArgumentOutOfRangeException aoorEx)
        {
            logger.LogError(aoorEx, $"ArgumentOutOfRangeException occured while deleting room with roomId: {roomId}.\nERROR MESSAGE: {aoorEx.Message}");
            return StatusCode(400, "RoomId is out of range.");
        }
        catch (Exception e)
        {
            logger.LogError(e, $"Unexpected error occurred while deleting Room with ID: {roomId}.\nERROR MESSAGE: {e.Message}");
            return StatusCode(500, "An unexpected error occured.");
        }
    }
}

[ApiController]
[Route("api")]
[Authorize(Roles = "Admin")]
public class RoomTypeController(IRoomService roomService, ILogger<RoomTypeController> logger) : Controller
{
    /* ROOM TYPE ENDPOINTS */
    // api/roomtypes
    [HttpGet("roomtypes")]
    [ProducesResponseType(typeof(IEnumerable<RoomTypeGetDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<RoomTypeGetDTO>>> GetRoomTypes()
    {
        try
        {
            return Ok(await roomService.GetRoomTypesAsync());
        }
        catch (DbException db)
        {
            logger.LogError(db, $"DatabaseException occurred while getting room types.\nERROR MESSAGE: {db.Message}\n INNER MESSAGE: {db.InnerException?.Message}");
            return StatusCode(500, "A database error occured.");
        }
        catch (Exception e)
        {
            logger.LogError(e, $"Unexpected error occurred while getting room types.\nERROR MESSAGE: {e.Message}");
            return StatusCode(500, "An unexpected error occured.");
        }
    }
    
    // api/roomtype
    [HttpPost("roomtype")]
    [ProducesResponseType(typeof(RoomType), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<RoomTypeCreateDTO>>> CreateRoomType(RoomTypeCreateDTO roomTypeDto)
    {
        try
        {
            await roomService.CreateRoomTypeAsync(roomTypeDto);
            return Created();
        }
        catch (DbException db)
        {
            logger.LogError(db, $"DatabaseException occurred while creating room type.\nERROR MESSAGE: {db.Message}\n INNER MESSAGE: {db.InnerException?.Message}");
            return StatusCode(500, "A database error occured.");
        }
        catch (Exception e)
        {
            logger.LogError(e, $"Unexpected error occurred while creating room type.\nERROR MESSAGE: {e.Message}");
            return StatusCode(500, "An unexpected error occured.");
        }
    }
    
    // api/roomtype/5
    [HttpDelete("roomtype/{roomTypeId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int roomTypeId)
    {
        try
        {
            await roomService.DeleteRoomTypeAsync(roomTypeId);
            return NoContent();
        }
        catch (DbException db)
        {
            logger.LogError(db,
                $"DatabaseException occurred while deleting room type with roomTypeId: {roomTypeId}.\nERROR MESSAGE: {db.Message}\n INNER MESSAGE: {db.InnerException?.Message}");
            return StatusCode(500, "A database error occured.");
        }
        catch (ArgumentOutOfRangeException aoorEx)
        {
            logger.LogError(aoorEx, $"ArgumentOutOfRangeException occured while deleting room type with roomTypeId: {roomTypeId}.\nERROR MESSAGE: {aoorEx.Message}");
            return StatusCode(400, "roomTypeId is out of range.");
        }
        catch (KeyNotFoundException knfEx)
        {
            logger.LogError(knfEx, $"KeyNotFoundException occured while deleting room type with roomTypeId: {roomTypeId}.\nERROR MESSAGE: {knfEx.Message}");
            return StatusCode(404, $"Room type not found with roomTypeId: {roomTypeId}.");
        }
        catch (Exception e)
        {
            logger.LogError(e, $"Unexpected error occurred while deleting room type with roomTypeId: {roomTypeId}.\nERROR MESSAGE: {e.Message}");
            return StatusCode(500, "An unexpected error occured.");
        }
    }
}