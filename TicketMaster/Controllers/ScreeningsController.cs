using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Humanizer;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketMaster.DataContext.Context;
using TicketMaster.DataContext.Models;
using TicketMaster.DataContext.UnitsOfWork;
using TicketMaster.Services.DTOs;

namespace TicketMaster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreeningsController : ControllerBase
    {
        private UnitOfWork unitOfWork;
        private IMapper mapper;

        public ScreeningsController(UnitOfWork _unitOfWork, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }

        // GET: api/Screenings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScreeningGetDTO>>> GetScreenings()
        {
            var screenings = await unitOfWork.ScreeningRepository.GetAsync(
                //includedProperties: ["Film", "Tickets"]
                );
            return mapper.Map<List<ScreeningGetDTO>>(screenings);
        }

        // GET: api/Screenings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ScreeningGetDTO>> GetScreening(int id)
        {
            var screening = await unitOfWork.ScreeningRepository.GetByIdAsync(
                 id,
                 includedReferences: ["Film"],
                 includedCollections: ["Tickets"]
                 );

            if (screening == null)
            {
                return NotFound();
            }
            return mapper.Map<ScreeningGetDTO>(screening);
        }

        // PUT: api/Screenings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutScreening(int id, ScreeningPutDTO dto)
        {
            Screening? screening = await unitOfWork.ScreeningRepository.GetByIdAsync(id);

            if (screening == null)
            {
                return NotFound();
            }

            var tickets = await unitOfWork.TicketRepository.GetAsync(
                includedProperties: ["Screening"]
                );

            //_context.Entry(@event).State = EntityState.Modified;
            //_unitOfWork.EventRepository.Update(@event);
            mapper.Map(dto, screening);

            try
            {
                //await _context.SaveChangesAsync();
                await unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ScreeningExists(id))
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

        // POST: api/Screenings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostScreening(ScreeningPostDTO screening)
        {
            /*if (screening.RoomId != null)
            {
                Room? room = await unitOfWork.RoomRepository.GetByIdAsync(screening.RoomId);
                if (room == null)
                {
                    return BadRequest("Room does not exist");
                }
            }
            if (screening.FilmId != null)
            {
                Film? film = await unitOfWork.FilmRepository.GetByIdAsync(screening.FilmId);
                if (film == null)
                {
                    return BadRequest("Film does not exist");
                }
            }*/

            Screening newScreening = mapper.Map<Screening>(screening);

            await unitOfWork.ScreeningRepository.InsertAsync(newScreening);
            await unitOfWork.SaveAsync();

            return Created();
        }

        // DELETE: api/Screenings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScreening(int id)
        {
            await unitOfWork.ScreeningRepository.DeleteByIdAsync(id);
            await unitOfWork.SaveAsync();

            return NoContent();
        }

        private async Task<bool> ScreeningExists(int id)
        {
            return await unitOfWork.ScreeningRepository.GetByIdAsync(id) != null;
        }
    }
}
