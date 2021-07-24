using MongoDB.Driver;
using SocialNet.Persistence.DataModels;
using SocialNet.Persistence.DBConfiguration;
using Microsoft.Extensions.Logging;

namespace SocialNet.Persistence.Repository
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
