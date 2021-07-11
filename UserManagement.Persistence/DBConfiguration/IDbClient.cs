using MongoDB.Driver;
using UserManagement.Persistence.DataModels;

namespace UserManagement.Persistence.DBConfiguration
{
    public interface IDbClient
    {
        IMongoCollection<T> DbCollection<T>() where T : BaseModel;
    }
}
