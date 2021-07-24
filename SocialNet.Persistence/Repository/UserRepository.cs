using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using SocialNet.Persistence.DataModels;
using SocialNet.Persistence.DBConfiguration;
using SocialNet.Services.IRepository;
using SocialNet.Services.Services;
using SocialNet.Services.Services.ResponseModels;
using SocialNet.Services.Services.RequestModels;

namespace SocialNet.Persistence.Repository
{
    public class UserRepository : ConfigRepository<User>, IUserRepository
    {
        private readonly IEncryptionServices _encryptionServices;

        public UserRepository(IDbClient dbClient, ILogger logger, IEncryptionServices encryptionServices)
            : base(dbClient, logger)
        {
            _encryptionServices = encryptionServices;
        }

        public async Task<UserDto> AddAsync(UserRegistration registration)
        {
            try
            {
                _encryptionServices.CreatePasswordHash(
                    registration.Password,
                    out byte[] passwordHash,
                    out byte[] passwordSalt);

                User entity = new()
                {
                    FirstName = registration.FirstName.Trim(),
                    LastName = registration.LastName.Trim(),
                    UserName = registration.UserName.Trim().ToLower(),
                    Email = registration.Email.Trim().ToLower(),
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                };

                await Collection.InsertOneAsync(entity);
                return MapUserAndGetDto(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{RepositoryName} Add method error", typeof(UserRepository));
                return new UserDto();
            }
        }

        public async Task<UserDto> GetByEmail(string email)
        {
            try
            {
                email = email?.Trim().ToLower();

                IAsyncCursor<User> cursor = await Collection.FindAsync(e => e.Email.Equals(email));
                User user = cursor.FirstOrDefault();
                return MapUserAndGetDto(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{RepositoryName} GetByEmail method error", typeof(UserRepository));
                return null;
            }
        }

        public async Task<UserDto> GetByUsernameAsync(string username)
        {
            try
            {
                username = username?.Trim().ToLower();
                IAsyncCursor<User> cursor = await Collection.FindAsync(e => e.Email.Equals(username));
                User user = cursor.FirstOrDefault();
                return MapUserAndGetDto(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{RepositoryName} GetByUsernameAsync method error", typeof(UserRepository));
                return null;
            }
        }

        public async Task<Tuple<bool, string>> VerifyUserCredentials(UserCredential userCreds)
        {
            try
            {
                userCreds.UserName = userCreds.UserName.Trim().ToLower();

                IAsyncCursor<User> cursor = await Collection.FindAsync(e => e.UserName.Equals(userCreds.UserName));
                User user = cursor.FirstOrDefault();
                var passwordIsVerified = _encryptionServices.VerifyPasswordHash(
                    userCreds.Password,
                    user.PasswordHash,
                    user.PasswordSalt);

                return new Tuple<bool, string>(
                    passwordIsVerified,
                    passwordIsVerified ? user.Id : "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{RepositoryName} GetByUserName method error", typeof(UserRepository));
                return new Tuple<bool, string>(false, "");
            }
        }

        public async Task<UserDto> GetByIdAsync(string id)
        {
            try
            {
                IAsyncCursor<User> cursor = await Collection.FindAsync(e => e.Id.Equals(id));
                User user = cursor.FirstOrDefault();
                return MapUserAndGetDto(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{RepositoryName} GetById method error", typeof(UserRepository));
                return null;
            }
        }

        private static UserDto MapUserAndGetDto(User user)
        {
            return user == null
                ? null
                : new UserDto {
                    Id = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                };
        }
    }
}
