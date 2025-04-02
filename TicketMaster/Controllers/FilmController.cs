using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
    public class FilmController : ControllerBase
    {
        private UnitOfWork _unitOfWork;
        private IMapper _mapper;

        public FilmController(UnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Film
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FilmGetDTO>>> GetFilms()
        {
            var films = await _unitOfWork.FilmRepository.GetAsync(
                //includedProperties: ["Screenings"]
                );
            return _mapper.Map<List<FilmGetDTO>>(films);
        }

        // GET: api/Film/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FilmGetDTO>> GetFilm(int id)
        {
            var film = await _unitOfWork.FilmRepository.GetByIdAsync(id,
                includedCollections: ["Screenings"]
                );

            if (film == null)
            {
                return NotFound("Film not found");
            }

            return _mapper.Map<FilmGetDTO>(film);
        }

        // PUT: api/Film/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilm(int id, FilmPutDTO film)
        {
            Film? f = await _unitOfWork.FilmRepository.GetByIdAsync(id);
            if (film == null)
            {
                return BadRequest("Film was empty");
            }

            if (film.Length != null && film.Length <= 0)
            {
                return BadRequest("Length can't be negative or 0 length");
            }
            if ((film.AgeRating < 0 || film.AgeRating > 18) && film.SetAgeRating)
            {
                return BadRequest("Age rating must be between 0 and 18 including 0 and 18");
            }

            _mapper.Map(film, f);

            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmExists(id))
                {
                    return NotFound("Film not found");
                }
                else
                {
                    throw;
                }
            }
            return Content("Film updated");
            return Ok();
        }

        // POST: api/Film
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FilmPostDTO>> PostFilm(FilmPostDTO film)
        {
            if(film == null)
            {
                return BadRequest("Empty film");
            }
            if(film.Length <= 0)
            {
                return BadRequest("Length can't be negative or 0 length");
            }
            if (film.AgeRating < 0 || film.AgeRating > 18)
            {
                return BadRequest("Age rating must be between 0 and 18 including 0 and 18");
            }
            if (film.Title == null || film.Director == null || film.Genre == null || film.Description == null)
            {
                return BadRequest("Title, Director, Genre and Description must be filled");
            }

            Film newFilm = _mapper.Map<Film>(film);

            await _unitOfWork.FilmRepository.InsertAsync(newFilm);
            await _unitOfWork.SaveAsync();

            return Content("Film created"); 
            return Created();
        }

        // DELETE: api/Film/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilm(int id)
        {
            await _unitOfWork.FilmRepository.DeleteByIdAsync(id);
            await _unitOfWork.SaveAsync();
            
            return Content("Film deleted");
            return NoContent();
        }

        private bool FilmExists(int id)
        {
            return _unitOfWork.FilmRepository.GetByIdAsync(id) != null;
        }
    }
}
