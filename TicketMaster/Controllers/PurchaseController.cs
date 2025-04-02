using System.Data.Common;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TicketMaster.DataContext.Context;
using TicketMaster.DataContext.Models;
using TicketMaster.DataContext.UnitsOfWork;
using TicketMaster.Services.DTOs;

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
    public async Task<ActionResult<IEnumerable<PurchaseGetDto>>> GetPurchases()
    {
        var purchases = await _unitOfWork.PurchaseRepository.GetAsync(
            includedProperties: ["User", "Tickets"]
            );
        return _mapper.Map<List<PurchaseGetDto>>(purchases);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PurchaseGetDto>> GetPurchase(int id)
    {
        var purchase = await _unitOfWork.PurchaseRepository.GetByIdAsync(
            id,
            includedReferences: ["User"],
            includedCollections: ["Tickets"]);
        if (purchase == null)
        {
            return NotFound();
        }
        return _mapper.Map<PurchaseGetDto>(purchase);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Purchase purchase)
    {
        try
        {
            var result = _unitOfWork.PurchaseRepository.InsertAsync(purchase);
            _unitOfWork.Save();
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutPurchase(int id, PurchasePutDto purchase)
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