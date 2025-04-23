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
using TicketMaster.Services.DTOs.TicketDTOs;

namespace TicketMaster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private UnitOfWork unitOfWork;
        private IMapper mapper;

        public TicketsController(UnitOfWork _unitOfWork, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }

        // GET: api/Tickets
        [HttpGet]
        [Authorize(Roles = "Admin, Cashier")]
        public async Task<ActionResult<IEnumerable<TicketGetDTO>>> GetTickets()
        {
            var tickets = await unitOfWork.TicketRepository.GetAsync(
                includedProperties: ["Purchase", "Screening"]
                );
            return mapper.Map<List<TicketGetDTO>>(tickets);
        }

        // GET: api/Tickets/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Cashier, Customer")]
        public async Task<ActionResult<TicketGetDTO>> GetTicket(int id)
        {
            var ticket = await unitOfWork.TicketRepository.GetByIdAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }

            await unitOfWork.TicketRepository.GetByIdAsync(id, includedReferences: ["Purchase"]);
            await unitOfWork.TicketRepository.GetByIdAsync(id, includedReferences: ["Screening"]);

            return mapper.Map<TicketGetDTO>(ticket);
        }

        // PUT: api/Tickets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutTicket(int id, TicketPutDTO dto)
        {
            Ticket? ticket = await unitOfWork.TicketRepository.GetByIdAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }

            mapper.Map(dto, ticket);
            Screening? screening = await unitOfWork.ScreeningRepository.GetByIdAsync(@ticket.ScreeningId);
            if (screening == null)
            {
                return BadRequest("Screening does not exist");
            }
            else
            {
                Room room = await unitOfWork.RoomRepository.GetByIdAsync(screening.RoomId);
                if (@ticket.SeatRow > room.MaxSeatRow || @ticket.SeatRow < 0 || ticket.SeatColumn > room.MaxSeatColumn || ticket.SeatColumn < 0)
                {
                    return BadRequest("Incorrect Seat Assignment");
                }
            }

            try
            {
                await unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/Tickets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> PostTicket(TicketPostDTO @ticket)
        {
            if (@ticket.PurchaseId != null)
            {
                Purchase? purchase = await unitOfWork.PurchaseRepository.GetByIdAsync(@ticket.PurchaseId.Value);
                if (purchase == null)
                {
                    return BadRequest("Purchase does not exist");
                }
            }

            Screening? screening = await unitOfWork.ScreeningRepository.GetByIdAsync(@ticket.ScreeningId);
            if (screening == null) 
            {
                return BadRequest("Screening does not exist");
            }
            else
            {
                Room room = await unitOfWork.RoomRepository.GetByIdAsync(screening.RoomId);
                if (@ticket.SeatRow > room.MaxSeatRow || @ticket.SeatRow < 0 || ticket.SeatColumn > room.MaxSeatColumn || ticket.SeatColumn < 0)
                {
                    return BadRequest("Incorrect Seat Assignment");
                }
            }

            Ticket newTicket = mapper.Map<Ticket>(@ticket);

            await unitOfWork.TicketRepository.InsertAsync(newTicket);
            await unitOfWork.SaveAsync();

            return Created();
        }

        // DELETE: api/Tickets/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            await unitOfWork.TicketRepository.DeleteByIdAsync(id);
            await unitOfWork.SaveAsync();

            return NoContent();
        }

        private bool TicketExists(int id)
        {
            return unitOfWork.TicketRepository.GetByIdAsync(id) != null;
        }
    }
}
