using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Persistence.DataModels;
using UserManagement.Persistence.DBConfiguration;
using UserManagement.Persistence.IRepository;

namespace UserManagement.Persistence.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(IDbClient dbClient, ILogger logger) : base(dbClient, logger) { }

        public override async Task<User> Add(User entity)
        {
            try
            {
                await Collection.InsertOneAsync(entity);
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{RepositoryName} Add method error", typeof(UserRepository));
                return new User();
            }
        }

        public override async Task<IEnumerable<User>> All()
        {
            try
            {
                IAsyncCursor<User> cursor = await Collection.FindAsync(e => true);
                IEnumerable<User> users = cursor.ToEnumerable();
                return users;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{RepositoryName} All method error", typeof(UserRepository));
                return new List<User>();
            }
        }

        public override Task<bool> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetByEmail(string email)
        {
            try
            {
                IAsyncCursor<User> cursor = await Collection.FindAsync(e => e.Email.Equals(email));
                return cursor.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{RepositoryName} GetByEmail method error", typeof(UserRepository));
                return new User();
            }
        }

        public async Task<User> GetByUserName(string username)
        {
            try
            {
                IAsyncCursor<User> cursor = await Collection.FindAsync(e => e.UserName.Equals(username));
                return cursor.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{RepositoryName} GetByUserName method error", typeof(UserRepository));
                return new User();
            }
        }

        public override async Task<User> GetByIdAsync(string id)
        {
            try
            {
                IAsyncCursor<User> cursor = await Collection.FindAsync(e => e.Id.Equals(id));
                return cursor.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{RepositoryName} GetById method error", typeof(UserRepository));
                return null;
            }
        }

        public override Task<bool> Upsert(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
