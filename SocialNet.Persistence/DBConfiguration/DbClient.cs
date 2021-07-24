using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.ComponentModel.DataAnnotations.Schema;
using SocialNet.Persistence.DataModels;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace SocialNet.Persistence.DBConfiguration
{
    public class DbClient : IDbClient
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;

        public DbClient(IOptions<DbConfig> dbConfig)
        {
            _client = new MongoClient(dbConfig.Value.Connection_String);
            _database = _client.GetDatabase(dbConfig.Value.Database_Name);
        }

        public IMongoCollection<T> DbCollection<T>() where T : BaseModel
        {
            IEnumerable<TableAttribute> tableAttributes = typeof(T).GetCustomAttributes<TableAttribute>(true);
            string tableName = tableAttributes.FirstOrDefault().Name;
            IMongoCollection<T> collection = _database.GetCollection<T>(tableName);
            return collection;
        }
    }
}
