using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketMaster.DataContext.Entities;
using TicketMaster.DataContext.Context;

namespace TicketMaster.Services
{
    public interface IFilmService
    {
        List<Film> List();
    }
    public class FilmService : IFilmService
    {
        private readonly AppDbContext _context;

        public FilmService(AppDbContext context)
        {
            _context = context;
        }

        public List<Film> List()
        {
            return _context.Films.ToList(); 
        }
    }
}
