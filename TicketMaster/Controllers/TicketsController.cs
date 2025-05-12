using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Humanizer;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketMaster.DataContext.Context;
using TicketMaster.DataContext.Models;
using TicketMaster.DataContext.UnitsOfWork;
using TicketMaster.Services;
using TicketMaster.Services.DTOs.TicketDTOs;

namespace TicketMaster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService ticketService;
        public TicketsController(ITicketService _ticketService)
        {
            ticketService = _ticketService;
        }

        // GET: api/Tickets
        [HttpGet]
        [Authorize(Roles = "Admin, Cashier")]
        public async Task<ActionResult<IEnumerable<TicketGetDTO>>> GetTickets()
        {
            return await ticketService.GetTicketsAsync();
        }
        
        // GET: api/Tickets/screening/5
        [HttpGet("screening/{screeningId}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<TicketGetDTO>>> GetTicketsByScreeningId(int screeningId)
        {
            try
            {
                return await ticketService.GetTicketsByScreeningIdAsync(screeningId);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        // GET: api/Tickets/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<TicketGetDTO>> GetTicket(int id)
        {
            try
            {
                return await ticketService.GetTicketByIdAsync(id);
            }
            catch (KeyNotFoundException e) 
            {
                return NotFound(e.Message);
            }
        }

        // PUT: api/Tickets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutTicket(int id, TicketPutDTO dto)
        {
            try
            {
                await ticketService.PutTicketAsync(id, dto);
                return Ok();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: api/Tickets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> PostTicket(TicketPostDTO @ticket)
        {
            try
            {
                await ticketService.PostTicketAsync(@ticket);
                return Created();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/Tickets/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            await ticketService.DeleteTicketAsync(id);
            return NoContent();
        }

        [HttpPut("{id}/validate")]
        [Authorize(Roles = "Cashier")]
        public async Task<ActionResult> ValidateTicket(int id)
        {
            try
            {
                await ticketService.Validate(id);
                return Ok("Ticket is valid and has been validated");
            }
            catch(KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch(ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
