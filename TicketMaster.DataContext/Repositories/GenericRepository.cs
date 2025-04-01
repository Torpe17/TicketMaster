using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketMaster.DataContext.Context;

namespace TicketMaster.DataContext.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected AppDbContext _dbContext;
        protected DbSet<TEntity> _dbset;

        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbset = _dbContext.Set<TEntity>();
        }

        //example: GetAsync(includedProperties: ["prop1", "prop2"]) will include the prop1 and prop2 properites
        public async Task<IEnumerable<TEntity>> GetAsync(string[]? includedProperties = null)
        {
            IQueryable<TEntity> query = _dbset;
            if (includedProperties != null)
            {
                foreach (var includedProperty in includedProperties)
                {
                    query = query.Include(includedProperty);
                }
            }
            return await query.ToListAsync();
        }

        //example: GetByIdAsync(id, includedReferences: ["ref1", "ref2"]) will include the ref1 and ref2 references
        //example: GetByIdAsync(id, includedCollections: ["col1", "col2"]) will include the col1 and col2 references
        public async Task<TEntity?> GetByIdAsync(int id, string[]? includedReferences = null, string[]? includedCollections = null)
        {
            TEntity? entity = await _dbset.FindAsync(id);
            if (entity == null)
            {
                return null;
            }

            List<Task> tasks = new List<Task>();

            if (includedReferences != null)
            {
                foreach (string reference in includedReferences)
                {
                    tasks.Add(_dbContext.Entry(entity).Reference(reference).LoadAsync());
                }
            }

            if (includedCollections != null)
            {
                foreach (string collection in includedCollections)
                {
                    tasks.Add(_dbContext.Entry(entity).Collection(collection).LoadAsync());
                }
            }

            await Task.WhenAll(tasks);

            return entity;
        }

        public async Task InsertAsync(TEntity entity)
        {
            await _dbset.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _dbset.Update(entity);
        }
        public async Task DeleteByIdAsync(int id)
        {
            TEntity? entity = await _dbset.FindAsync(id);
            if (entity != null)
            {
                _dbset.Remove(entity);
            }
        }
    }
}
