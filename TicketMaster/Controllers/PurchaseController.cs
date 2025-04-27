using System.Data.Common;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketMaster.DataContext.Context;
using TicketMaster.DataContext.Models;
using TicketMaster.DataContext.UnitsOfWork;
using TicketMaster.Services;
using TicketMaster.Services.DTOs;
using TicketMaster.Services.DTOs.PurchaseDTOs;

namespace TicketMaster.Controllers;

[ApiController]
[Route("api")]
[Authorize]
public class PurchaseController(IPurchaseService purchaseService, ILogger<PurchaseController> logger, IAuthorizationService authorizationService)
    : Controller
{
    [HttpGet("purchases")]
    [ProducesResponseType(typeof(IEnumerable<PurchaseGetDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize(Policy = "AdminOnly")]
    public async Task<ActionResult<IEnumerable<PurchaseGetDTO>>> GetPurchases()
    {
        try
        {
            return Ok(await purchaseService.GetPurchasesAsync());
        }
        catch (DbException db)
        {
            logger.LogError(db, $"DatabaseException occured while getting purchases.\nERROR MESSAGE: {db.Message}\nINNER MESSAGE: {db.InnerException?.Message}");
            return StatusCode(500, "A database error occured.");
        }
        catch (Exception e)
        {
            logger.LogError(e, $"Unexpected exception occurred while getting purchases.\nERROR MESSAGE: {e.Message}");
            return StatusCode(500, "An unexpected error occured.");
            
        }
    }
    
    [HttpGet("purchases/{userId:int}")]
    [ProducesResponseType(typeof(IEnumerable<PurchaseGetDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = "AdminOnly")]
    public async Task<ActionResult<IEnumerable<PurchaseGetDTO>>> GetPurchasesByUserId(int userId)
    {
        try
        {
            return Ok(await purchaseService.GetPurchasesByUserIdAsync(userId));
        }
        catch (DbException db)
        {
            logger.LogError(db,
                $"DatabaseException occured while getting purchases with userId: {userId}.\nERROR MESSAGE: {db.Message}\nINNER MESSAGE: {db.InnerException?.Message}");
            return StatusCode(500, "A database error occured.");
        }
        catch (ArgumentOutOfRangeException aoorEx)
        {
            logger.LogError(aoorEx,
                $"ArgumentOutOfRangeException occured while getting purchases with userId: {userId}.\nERROR MESSAGE: {aoorEx.Message}");
            return StatusCode(400, "UserId is out of range.");
        }
        catch (KeyNotFoundException knfEx)
        {
            logger.LogError(knfEx,
                $"KeyNotFoundException occured while getting purchases with userId: {userId}.\nERROR MESSAGE: {knfEx.Message}");
            return StatusCode(404, $"Purchases with userId: {userId} not found.");

        }
        catch (Exception e)
        {
            logger.LogError(e, $"Unexpected exception occurred while getting purchases with userId: {userId}.\nERROR MESSAGE: {e.Message}");
            return StatusCode(500, "An unexpected error occured.");
            
        }
    }
    
    [HttpGet("myPurchases")]
    [ProducesResponseType(typeof(IEnumerable<PurchaseGetDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Roles = "User")]
    public async Task<ActionResult<IEnumerable<PurchaseGetDTO>>> GetOwnPurchases()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int id))
        {
            return Unauthorized();
        }
        
        try
        {
            return Ok(await purchaseService.GetPurchasesByUserIdAsync(id));
        }
        catch (DbException db)
        {
            logger.LogError(db,
                $"DatabaseException occured while getting purchases with userId: {id}.\nERROR MESSAGE: {db.Message}\nINNER MESSAGE: {db.InnerException?.Message}");
            return StatusCode(500, "A database error occured.");
        }
        catch (ArgumentOutOfRangeException aoorEx)
        {
            logger.LogError(aoorEx,
                $"ArgumentOutOfRangeException occured while getting purchases with userId: {id}.\nERROR MESSAGE: {aoorEx.Message}");
            return StatusCode(400, "UserId is out of range.");
        }
        catch (KeyNotFoundException knfEx)
        {
            logger.LogError(knfEx,
                $"KeyNotFoundException occured while getting purchases with userId: {id}.\nERROR MESSAGE: {knfEx.Message}");
            return StatusCode(404, $"Purchases with userId: {id} not found.");

        }
        catch (Exception e)
        {
            logger.LogError(e, $"Unexpected exception occurred while getting purchases with userId: {id}.\nERROR MESSAGE: {e.Message}");
            return StatusCode(500, "An unexpected error occured.");
            
        }
    }

    [HttpGet("purchase/{purchaseId:int}")]
    [ProducesResponseType(typeof(PurchaseGetDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = "Admin, User, Cashier")]
    public async Task<ActionResult<PurchaseGetByIdDTO>> GetPurchaseById(int purchaseId)
    {
        try
        {
            return Ok(await purchaseService.GetPurchaseByIdAsync(purchaseId));
        }
        catch (DbException db)
        {
            logger.LogError(db,
                $"DatabaseException occurred while getting Purchase by ID: {purchaseId}.\nERROR MESSAGE: {db.Message}\nINNER MESSAGE: {db.InnerException?.Message}");
            return StatusCode(500, "A database error occured.");
        }
        catch (ArgumentOutOfRangeException aoorEx)
        {
            logger.LogError(aoorEx,
                $"ArgumentOutOfRangeException occured while getting purchase by purchaseId: {purchaseId}.\nERROR MESSAGE: {aoorEx.Message}");
            return StatusCode(400, "PurchaseId is out of range.");
        }
        catch (KeyNotFoundException knfEx)
        {
            logger.LogError(knfEx,
                $"KeyNotFoundException occured while getting purchase by purchaseId: {purchaseId}.\nERROR MESSAGE: {knfEx.Message}");
            return StatusCode(404, $"Purchase not found with purchaseId: {purchaseId}.");
        }
        catch (Exception e)
        {
            logger.LogError(e, $"Unexpected error occurred while getting Purchase by ID: {purchaseId}.\nERROR MESSAGE: {e.Message}");
            return StatusCode(500, "An unexpected error occured.");
        }
        
    }

    [HttpPost("purchase")]
    [ProducesResponseType(typeof(PurchaseGetDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(PurchaseGetDTO), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(PurchaseGetDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    public async Task<IActionResult> PostPurchase(PurchasePostDTO purchase)
    {
        try
        {
            bool isAuthenticated = User.Identity.IsAuthenticated;
            int? userId = null;
            if (isAuthenticated) userId = int.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            await purchaseService.CreatePurchase(purchase, isAuthenticated, userId);
            return Created();
        }
        catch (KeyNotFoundException e)
        {
            return StatusCode(404, e.Message);
        }
        catch (ArgumentException e)
        {
            return StatusCode(400, e.Message);
        }
        catch (DbException db)
        {
            logger.LogError(db, $"DatabaseException occured while creating Purchase.\nERROR MESSAGE: {db.Message}\nINNER MESSAGE: {db.InnerException?.Message}");
            return StatusCode(500, "A database error occured.");
        }
        catch (Exception e)
        {
            logger.LogError(e, $"Unexpected error occurred while creating Purchase.\nERROR MESSAGE: {e.Message}");
            return StatusCode(500, "An unexpected error occured.");
        }
    }

    [HttpDelete("purchase/{purchaseId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public async Task<IActionResult> Delete(int purchaseId)
    {
        try
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId)) { return BadRequest("Invalid user ID format."); }
            var isAdminOrCashier = User.IsInRole("Admin") || User.IsInRole("Cashier");
            
            bool isAllowed = await purchaseService.CanUserDeletePurchaseAsync(purchaseId, userId, isAdminOrCashier);
            if (!isAllowed) { return Forbid(); }
            
            await purchaseService.DeletePurchase(purchaseId);
            return NoContent();
        }
        catch (DbException db)
        {
            logger.LogError(db,
                $"DatabaseException occured while deleting Purchase with ID: {purchaseId}.\nERROR MESSAGE: {db.Message}\nINNER MESSAGE: {db.InnerException?.Message}");
            return StatusCode(500, "A database error occured.");
        }
        catch (ArgumentOutOfRangeException aoorEx)
        {
            logger.LogError(aoorEx,
                $"ArgumentOutOfRangeException occured while deleting Purchase with ID: {purchaseId}.\nERROR MESSAGE: {aoorEx.Message}");
            return StatusCode(400, "PurchaseId is out of range.");
        }
        catch (Exception e)
        {
            logger.LogError(e, $"Unexpected error occurred while deleting Purchase with ID: {purchaseId}.\nERROR MESSAGE: {e.Message}");
            return StatusCode(500, "An unexpected error occured.");
        }
    }
}