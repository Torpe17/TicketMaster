using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketMaster.DataContext.Repositories
{
    interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAsync(string[]? includedProperties = null);
        Task<TEntity?> GetByIdAsync(int id, string[]? includedReferences = null, string[]? includedCollections = null);
        Task InsertAsync(TEntity entity);
        void Update(TEntity entity);
        Task DeleteByIdAsync(int id);
    }
}
