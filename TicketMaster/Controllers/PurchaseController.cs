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
    public async Task<List<ActionResult<IEnumerable<PurchaseGetDto>>>> GetPurchases()
    {
        var purchases = await _unitOfWork.PurchaseRepository.GetAsync();
        return _mapper.Map<List<PurchaseGetDto>>(purchases);
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

    [HttpPut]
    public IActionResult Put([FromBody] Purchase purchase)
    {
        try
        {
            _unitOfWork.PurchaseRepository.Update(purchase);
            _unitOfWork.Save();
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
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