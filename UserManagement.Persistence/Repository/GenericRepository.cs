using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Persistence.DataModels;
using UserManagement.Persistence.DBConfiguration;
using UserManagement.Persistence.IRepository;
using Microsoft.Extensions.Logging;

namespace UserManagement.Persistence.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
    {
        private readonly IDbClient _dbClient;
        protected readonly ILogger _logger;
        public IMongoCollection<T> Collection { get; private set; }

        public GenericRepository(
            IDbClient dbClient,
            ILogger logger)
        {
            _logger = logger;
            _dbClient = dbClient;
            Collection = _dbClient.DbCollection<T>();
        }

        public virtual Task<IEnumerable<T>> All()
        {
            throw new NotImplementedException();
        }

        public virtual Task<T> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<T> AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> Upsert(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
