using MongoDB.Driver;
using UserManagement.DataModels;

namespace UserManagement.DBConfiguration
{
    public interface IDbClient
    {
        IMongoCollection<User> GetUserCollection();
    }
}
