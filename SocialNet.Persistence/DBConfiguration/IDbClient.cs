using MongoDB.Driver;
using SocialNet.Persistence.DataModels;

namespace SocialNet.Persistence.DBConfiguration
{
    public interface IDbClient
    {
        IMongoCollection<T> DbCollection<T>() where T : BaseModel;
    }
}
