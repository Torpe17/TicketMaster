using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketMaster.DataContext.Models;
using TicketMaster.DataContext.Context;
using Microsoft.EntityFrameworkCore;
using TicketMaster.Services.DTOs;
using TicketMaster.Services.DTOs.FilmDTOs;
using TicketMaster.DataContext.UnitsOfWork;
using AutoMapper;
using TicketMaster.Services.DTOs.ScreeningDTOs;

namespace TicketMaster.Services
{
    public interface IFilmService
    {
        Task<List<FilmGetDTO>> GetAllAsync();
        Task<FilmGetDTO> GetByIdAsync(int id);
        Task<List<ScreeningGetDTO>> GetScreeningByFilmIdAsync(int filmId);
        Task<List<FilmGetDTO>> GetByNameAsync(string name);
        Task<List<FilmGetDTO>> GetByDateAsync(string date);
        Task<List<FilmGetDTO>> GetAfterDateAsync(string date);
        Task<List<FilmGetDTO>> GetByNameAndDateAsync(string name, string date, bool onDay = false);
        Task AddFilmAsync(FilmPostDTO film);
        Task UpdateFilmAsync(int id, FilmPutDTO film);
        Task DeleteFilmAsync(int id);
    }
    public class FilmService : IFilmService
    {
        private UnitOfWork _unitOfWork;
        private AppDbContext _context;
        private IMapper _mapper;
        public FilmService(UnitOfWork unitOfWork, IMapper mapper, AppDbContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<ScreeningGetDTO>> GetScreeningByFilmIdAsync(int filmId)
        {
            var film = await _context.Films.Include(x => x.Screenings).ThenInclude(x => x.RoomId).FirstOrDefaultAsync(x => x.Id == filmId);
            //var film = await _unitOfWork.FilmRepository.GetByIdAsync(filmId, includedCollections: ["Screenings"], includedReferences: ["Room"]);
            if (film == null)
            {
                throw new KeyNotFoundException($"Film (id: {filmId}) not found");
            }
            return _mapper.Map<List<ScreeningGetDTO>>(film.Screenings);
        }

        public async Task AddFilmAsync(FilmPostDTO film)
        {
            if (film == null)
                throw new ArgumentException("Empty film");
            if (film.Length <= 0)
                throw new ArgumentException("Length can't be negative or 0 length");
            if (film.AgeRating < 0 || film.AgeRating > 18)
                throw new ArgumentException("Age rating must be between 0 and 18 including 0 and 18");
            if (film.Title == null || film.Director == null || film.Genre == null || film.Description == null)
                throw new ArgumentException("Title, Director, Genre and Description must be filled");

            Film newFilm = _mapper.Map<Film>(film);

            if (film.PictureBytes != null)
            {
                newFilm.Picture = film.PictureBytes;
            }

            await _unitOfWork.FilmRepository.InsertAsync(newFilm);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteFilmAsync(int id)
        {
            await _unitOfWork.FilmRepository.DeleteByIdAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<List<FilmGetDTO>> GetAfterDateAsync(string date)
        {
            var films = await _unitOfWork.FilmRepository.GetAsync(includedProperties: ["Screenings"]);

            DateTime dateTime = DateTime.Parse(date);
            var filmsOnDate = films.Where(f => f.Screenings.Any(s => s.Date >= dateTime)).ToList();

            var result = _mapper.Map<List<FilmGetDTO>>(filmsOnDate);

            for (int i = 0; i < filmsOnDate.Count; i++)
            {
                if (filmsOnDate[i].Picture != null && filmsOnDate[i].Picture.Length > 0)
                {
                    result[i].PictureBase64 = Convert.ToBase64String(filmsOnDate[i].Picture);
                }
            }

            return _mapper.Map<List<FilmGetDTO>>(filmsOnDate);
        }
        public async Task<List<FilmGetDTO>> GetByNameAndDateAsync(string name, string date, bool onDay = false)
        {
            var films = await _unitOfWork.FilmRepository.GetAsync(includedProperties: ["Screenings"]);
            List<Film> filmsOnDate;
            DateTime dateTime = DateTime.Parse(date);
            if (onDay)
            {
                filmsOnDate = films.Where(f => f.Screenings.Any(s => s.Date.Date == dateTime)).ToList();
            }
            else
            {
                filmsOnDate = films.Where(f => f.Screenings.Any(s => s.Date >= dateTime)).ToList();
            }
             
            var filmsWithNameAndDate = filmsOnDate.Where(f => f.Title.ToLower().Contains(name.ToLower())).ToList();

            return _mapper.Map<List<FilmGetDTO>>(filmsWithNameAndDate);
        }

        public async Task<List<FilmGetDTO>> GetAllAsync()
        {
            var films = await _unitOfWork.FilmRepository.GetAsync(
                 //includedProperties: ["Screenings"]
                 );

            var filmDtos = films.Select(film =>
            {
                var dto = _mapper.Map<FilmGetDTO>(film);
                if (film.Picture != null && film.Picture.Length > 0)
                {
                    dto.PictureBase64 = Convert.ToBase64String(film.Picture);
                }

                return dto;
            }).ToList();

            return filmDtos;
        }

        public async Task<List<FilmGetDTO>> GetByDateAsync(string date)
        {
            var films = await _unitOfWork.FilmRepository.GetAsync(includedProperties: ["Screenings"]);

            DateTime dateTime = DateTime.Parse(date);
            var filmsOnDate = films.Where(f => f.Screenings.Any(s => s.Date.Date == dateTime)).ToList();
            return _mapper.Map<List<FilmGetDTO>>(filmsOnDate);
        }

        public async Task<FilmGetDTO> GetByIdAsync(int id)
        {
            var film = await _unitOfWork.FilmRepository.GetByIdAsync(id/*,
                includedCollections: ["Screenings"]*/
                );

            if (film == null)
            {
                throw new KeyNotFoundException($"Film (id: {id}) not found");
            }
            var dto = _mapper.Map<FilmGetDTO>(film);
            if (film.Picture != null)
            {
                dto.PictureBase64 = Convert.ToBase64String(film.Picture);
            }
            return _mapper.Map<FilmGetDTO>(film);
        }

        public async Task<List<FilmGetDTO>> GetByNameAsync(string name)
        {
            var films = await _unitOfWork.FilmRepository.GetAsync(includedProperties: ["Screenings"]);

            var filmsWithName = films.Where(f => f.Title.ToLower().Contains(name.ToLower())).ToList();
            return _mapper.Map<List<FilmGetDTO>>(filmsWithName);
        }


        public async Task UpdateFilmAsync(int id, FilmPutDTO film)
        {
            if (film == null)
                throw new ArgumentException("Film was empty");

            Film? f = await _unitOfWork.FilmRepository.GetByIdAsync(id);
            if (f == null)
                throw new ArgumentException("Film not found");

            if (film.Length != null && film.Length <= 0)
                throw new ArgumentException("Length can't be negative or 0 length");
            if ((film.AgeRating < 0 || film.AgeRating > 18) && film.SetAgeRating)
                throw new ArgumentException("Age rating must be between 0 and 18 including 0 and 18");

            if (film.PictureBase64 != null)
            {
                try
                {
                    // Remove data URI prefix if present
                    var base64Data = film.PictureBase64.StartsWith("data:image")
                        ? film.PictureBase64.Split(',')[1]
                        : film.PictureBase64;

                    f.Picture = Convert.FromBase64String(base64Data);
                }
                catch (FormatException)
                {
                    throw new ArgumentException("Invalid image format. Please provide a valid Base64 string.");
                }
            }
            else if (film.RemovePicture)
            {
                f.Picture = null;
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
                    throw new KeyNotFoundException($"Film not found with id: {id}");
                }
                else
                {
                    throw;
                }
            }
            //return Created("Film updated");
        }
        private bool FilmExists(int id)
        {
            return _unitOfWork.FilmRepository.GetByIdAsync(id) != null;
        }
    }
}
