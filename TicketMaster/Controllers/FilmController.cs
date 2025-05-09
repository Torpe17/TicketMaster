using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using TicketMaster.DataContext.Context;
using TicketMaster.DataContext.Models;
using TicketMaster.DataContext.UnitsOfWork;
using TicketMaster.Services;
using TicketMaster.Services.DTOs;
using TicketMaster.Services.DTOs.FilmDTOs;
using TicketMaster.Services.DTOs.ScreeningDTOs;

namespace TicketMaster.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController : ControllerBase
    {
        private IFilmService _filmService;

        public FilmController(IFilmService filmService)
        {
            _filmService = filmService;
        }

        // GET: api/Film
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<FilmGetDTO>>> GetAllFilms()
        {
            return await _filmService.GetAllAsync();
        }

        // GET: api/Film/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<FilmGetDTO>> GetFilmByID(int id)
        {
            try
            {
                return await _filmService.GetByIdAsync(id);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }


        [HttpGet("{filmId}/screenings")]
        [AllowAnonymous]
        public async Task<ActionResult<List<ScreeningGetDTO>>> GetScreeningsByFilmId(int filmId)
        {
            try
            {
                return await _filmService.GetScreeningByFilmIdAsync(filmId);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }

        }

        [HttpGet("on/{date}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<FilmGetDTO>>> GetFilmsByDate(string date)
        {
            return await _filmService.GetByDateAsync(date);
        }

        [HttpGet("after/{date}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<FilmGetDTO>>> GetFilmsAfterDate(string date)
        {
            return await _filmService.GetAfterDateAsync(date);
        }
        [HttpGet("name")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<FilmGetDTO>>> GetFilmByName(string name)
        {
            return await _filmService.GetByNameAsync(name);
        }

        [HttpGet("NameAndDate")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<FilmGetDTO>>> GetFilmByNameAndDate(string name, string date, bool onDay)
        {
            return await _filmService.GetByNameAndDateAsync(name, date, onDay);
        }

        // PUT: api/Film/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutFilm(int id, FilmPutDTO film)
        {
            try
            {
                await _filmService.UpdateFilmAsync(id, film);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        // POST: api/Film
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<FilmPostDTO>> PostFilm(FilmPostDTO film)
        {
            try
            {
                if (film.Picture != null && film.Picture.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await film.Picture.CopyToAsync(memoryStream);
                        film.PictureBytes = memoryStream.ToArray();
                    }
                }
                await _filmService.AddFilmAsync(film);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Film created"); 
        }

        // DELETE: api/Film/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteFilm(int id)
        {
            await _filmService.DeleteFilmAsync(id);
            return NoContent();
        }

            
    }
}
