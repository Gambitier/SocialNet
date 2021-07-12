using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserManagement.Persistence.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> All();
        Task<T> GetByIdAsync(string id);
        Task<T> AddAsync(T entity);
        Task<bool> Delete(string id);
        Task<bool> Upsert(T entity);
    }
}
