using MongoDB.Driver;
using UserManagement.Persistence.DataModels;
using UserManagement.Persistence.DBConfiguration;
using Microsoft.Extensions.Logging;

namespace UserManagement.Persistence.Repository
{
    public class ConfigRepository<T> where T : BaseModel
    {
        private readonly IDbClient _dbClient;
        protected readonly ILogger _logger;
        public IMongoCollection<T> Collection { get; private set; }

        public ConfigRepository(
            IDbClient dbClient,
            ILogger logger)
        {
            _logger = logger;
            _dbClient = dbClient;
            Collection = _dbClient.DbCollection<T>();
        }
    }
}
