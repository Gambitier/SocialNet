using System.Threading.Tasks;
using UserManagement.Persistence.DataModels;

namespace UserManagement.Persistence.IRepository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByEmail(string email);
        Task<User> GetByUserName(string username);
    }
}
