using System.Data.Common;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketMaster.DataContext.Context;
using TicketMaster.DataContext.Models;
using TicketMaster.DataContext.UnitsOfWork;
using TicketMaster.Services.DTOs;
using TicketMaster.Services.DTOs.PurchaseDTOs;

namespace TicketMaster.Controllers;

[ApiController]
[Route("api/purchase")]
public class PurchaseController : Controller
{
    private readonly ILogger<PurchaseController> _logger;
    private UnitOfWork _unitOfWork;
    private IMapper _mapper;

    public PurchaseController(UnitOfWork unitOfWork, IMapper mapper,ILogger<PurchaseController> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }
    
    [HttpGet("getall")]
    [ProducesResponseType(typeof(IEnumerable<PurchaseGetDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<PurchaseGetDTO>>> GetPurchases()
    {
        try
        {
            var purchases = await _unitOfWork.PurchaseRepository.GetAsync(
                includedProperties: ["User", "Tickets"]
            );
            if (purchases == null || !purchases.Any()) { return Ok(Enumerable.Empty<PurchaseGetDTO>()); }
            
            return Ok(_mapper.Map<List<PurchaseGetDTO>>(purchases));
        }
        catch (DbException db)
        {
            _logger.LogError(db, "Unexpected error occurred in the database while getting all Purchases.");
            return StatusCode(500, "A database error occured.");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unexpected error occurred while getting all Purchases.");
            return StatusCode(500, "An unexpected error occured.");
            
        }
    }

    [HttpGet("get/{id}")]
    [ProducesResponseType(typeof(PurchaseGetDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PurchaseGetByIdDTO>> GetPurchase(int id)
    {
        if (id <= 0) return BadRequest("ID must be greater than zero.");
        // var purchase = await _unitOfWork.PurchaseRepository.GetByIdAsync(id, includedReferences: ["User"], includedCollections: ["Tickets"]); // race condition if both are set at the same time
        try
        {
            var purchase = await _unitOfWork.PurchaseRepository.GetByIdAsync(id);
            await _unitOfWork.PurchaseRepository.GetByIdAsync(id, includedReferences: ["User"]);
            await _unitOfWork.PurchaseRepository.GetByIdAsync(id, includedCollections: ["Tickets"]);

            if (purchase == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PurchaseGetByIdDTO>(purchase));
        }
        catch (DbException db)
        {
            _logger.LogError(db, $"Unexpected error occurred in the database while getting Purchase by ID: {id}.");
            return StatusCode(500, "A database error occured.");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Unexpected error occurred while getting Purchase by ID: {id}.");
            return StatusCode(500, "An unexpected error occured.");
        }
        
    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(PurchaseGetDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PostPurchase(PurchasePostDTO @purchase)
    {
        try
        {
            if (@purchase.UserId != null)
            {
                User? user = await _unitOfWork.UserRepository.GetByIdAsync(@purchase.UserId.Value);
                if (user == null) { return BadRequest("User does not exist."); }
            }

            Purchase newPurchase = _mapper.Map<Purchase>(@purchase);

            await _unitOfWork.PurchaseRepository.InsertAsync(newPurchase);
            await _unitOfWork.SaveAsync();

            return Created();
        }
        catch (DbException db)
        {
            _logger.LogError(db, $"Unexpected error occurred in the database while creating Purchase.");
            return StatusCode(500, "A database error occured.");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Unexpected error occurred while creating Purchase.");
            return StatusCode(500, "An unexpected error occured.");
        }
    }

    [HttpPut("update/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutPurchase(int id, PurchasePutDTO purchase)
    {
        try
        {
            if (id <= 0) return BadRequest("ID must be greater than zero.");
            Purchase? p = await _unitOfWork.PurchaseRepository.GetByIdAsync(id);
            if (p == null) return NotFound("Purchase specified by ID not found.");

            _mapper.Map(purchase, p);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
        catch (DbException db)
        {
            _logger.LogError(db, $"Unexpected error occurred in the database while updating Purchase with ID: {id}.");
            return StatusCode(500, "A database error occured.");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Unexpected error occurred while updating Purchase with ID: {id}.");
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
            await _unitOfWork.PurchaseRepository.DeleteByIdAsync(id);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }
        catch (DbException db)
        {
            _logger.LogError(db, $"Unexpected error occurred in the database while deleting Purchase with ID: {id}.");
            return StatusCode(500, "A database error occured.");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Unexpected error occurred while deleting Purchase with ID: {id}.");
            return StatusCode(500, "An unexpected error occured.");
        }
    }
}