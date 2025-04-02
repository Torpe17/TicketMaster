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
[Route("api/[controller]/[action]")]
public class PurchaseController : Controller
{
    private ILogger<PurchaseController> _logger;
    private UnitOfWork _unitOfWork;
    private IMapper _mapper;

    public PurchaseController(UnitOfWork unitOfWork, IMapper mapper,ILogger<PurchaseController> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PurchaseGetDTO>>> GetPurchases()
    {
        var purchases = await _unitOfWork.PurchaseRepository.GetAsync(
            includedProperties: ["User", "Tickets"]
            );
        return _mapper.Map<List<PurchaseGetDTO>>(purchases);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PurchaseGetByIdDTO>> GetPurchase(int id)
    {
        var purchase = await _unitOfWork.PurchaseRepository.GetByIdAsync(
            id,
            includedReferences: ["User"],
            includedCollections: ["Tickets"]);
        if (purchase == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<PurchaseGetByIdDTO>(purchase));
    }

    [HttpPost]
    public async Task<IActionResult> PostPurchase(PurchasePostDTO @purchase)
    {
        // TO-DO: errod handling
        if (@purchase.UserId != null)
        {
            User? user = await _unitOfWork.UserRepository.GetByIdAsync(@purchase.UserId.Value);
            if (user == null)
            {
                return BadRequest("User does not exist.");
            }
        }
        
        Purchase newPurchase = _mapper.Map<Purchase>(@purchase);
        
        await _unitOfWork.PurchaseRepository.InsertAsync(newPurchase);
        await _unitOfWork.SaveAsync();

        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutPurchase(int id, PurchasePutDTO purchase)
    {
        Purchase? p = await _unitOfWork.PurchaseRepository.GetByIdAsync(id);
        if (p == null) return NotFound();
        
        _mapper.Map(purchase, p);
        try
        {
            await _unitOfWork.SaveAsync();
        }
        catch (Exception e)
        {
            // TODO: better error handling
            return NotFound(e.Message);
        }

        return Ok();
    }
    
    [HttpDelete]
    public IActionResult Delete([FromBody] int id)
    {
        try
        {
            var result = _unitOfWork.PurchaseRepository.DeleteByIdAsync(id);
            _unitOfWork.Save();
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}