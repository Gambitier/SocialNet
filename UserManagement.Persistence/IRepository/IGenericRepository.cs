using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserManagement.Persistence.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> All();
        Task<T> GetById(string id);
        Task<T> Add(T entity);
        Task<bool> Delete(string id);
        Task<bool> Upsert(T entity);
    }
}
