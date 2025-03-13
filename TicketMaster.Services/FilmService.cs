using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketMaster.DataContext.Models;
using TicketMaster.DataContext.Context;
using Microsoft.EntityFrameworkCore;

namespace TicketMaster.Services
{
    public interface IFilmService
    {
        List<Film> List();
        Task AddAsync<T>(T entity) where T : class;
        Task<T> GetByIdAsync<T>(int id) where T : class;
        Task<List<T>> GetAllAsync<T>() where T : class;
        Task UpdateAsync<T>(T entity) where T : class;
        Task DeleteAsync<T>(T entity) where T : class;
    }
    public class FilmService : IFilmService
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

        public async Task AddAsync<T>(T entity) where T : class
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync<T>(int id) where T : class
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetAllAsync<T>() where T : class
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task UpdateAsync<T>(T entity) where T : class
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync<T>(T entity) where T : class
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
        
    }
}
