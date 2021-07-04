using Microsoft.Extensions.Options;
using MongoDB.Driver;
using UserManagement.DataModels;

namespace UserManagement.DBConfiguration
{
    public class DbClient : IDbClient
    {
        private readonly IMongoCollection<User> _users;

        public DbClient(IOptions<DbConfig> dbConfig)
        {
            var client = new MongoClient(dbConfig.Value.Connection_String);
            var database = client.GetDatabase(dbConfig.Value.Database_Name);
            _users = database.GetCollection<User>(dbConfig.Value.User_Collection_Name);
        }

        public IMongoCollection<User> GetUserCollection() => _users;
    }
}
