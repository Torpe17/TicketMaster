using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketMaster.DataContext.Models;
using TicketMaster.DataContext.Context;

namespace TicketMaster.Services
{
    public interface IFilmService
    {
        List<Film> List();
    }
    class FilmService : IFilmService
    {
        private readonly TicketMasterDbContext _context;

        public FilmService(TicketMasterDbContext context)
        {
            _context = context;
        }

        public List<Film> List()
        {
            return _context.Films.ToList();
        }
    }
}
