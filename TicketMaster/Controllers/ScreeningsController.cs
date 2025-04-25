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
using TicketMaster.Services.DTOs.ScreeningDTOs;

namespace TicketMaster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreeningsController : ControllerBase
    {
        private readonly IScreeningService screeningService;

        public ScreeningsController(IScreeningService _screeningService)
        {
            screeningService = _screeningService;
        }

        // GET: api/Screenings
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ScreeningGetDTO>>> GetScreenings()
        {
            return await screeningService.GetScreeningsAsync();
        }

        // GET: api/Screenings/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Cashier, Customer")]
        public async Task<ActionResult<ScreeningGetDTO>> GetScreening(int id)
        {
            try
            {
                return await screeningService.GetScreeningByIdAsync(id);
            }
            catch (KeyNotFoundException e) 
            {
                return NotFound(e.Message);
            }

        }

        // PUT: api/Screenings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutScreening(int id, ScreeningPutDTO dto)
        {
            try
            {
                await screeningService.PutScreeningAsync(id, dto);
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

        // POST: api/Screenings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> PostScreening(ScreeningPostDTO screening)
        {
            try
            {
                await screeningService.PostScreeningAsync(screening);
                return Created();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/Screenings/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteScreening(int id)
        {
            await screeningService.DeleteScreeningAsync(id);
            return NoContent();
        }
    }
}
