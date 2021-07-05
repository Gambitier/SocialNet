using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using UserManagement.DataModels;
using UserManagement.DBConfiguration;
using UserManagement.Exceptions;
using UserManagement.RequestModels;
using UserManagement.ResponseModels;

namespace UserManagement.Services
{
    public class UserServices : IUserServices
    {
        private readonly IMongoCollection<User> _users;
        private readonly IEncryptionServices _encryptionServices;

        public UserServices(IDbClient dbClient, IEncryptionServices encryptionServices)
        {
            _users = dbClient.GetUserCollection();
            _encryptionServices = encryptionServices;
        }

        public async Task<string> RegisterUserAsync(UserRegistration userRegistration)
        {
            await ValidateUsernameAsync(userRegistration.UserName);
            await ValidateEmailAsync(userRegistration.Email);

            _encryptionServices.CreatePasswordHash(
                userRegistration.Password,
                out byte[] passwordHash,
                out byte[] passwordSalt);

            var user = new User
            {
                FirstName = userRegistration.FirstName.Trim().ToLower(),
                LastName = userRegistration.LastName.Trim().ToLower(),
                UserName = userRegistration.UserName.Trim().ToLower(),
                Email = userRegistration.Email.Trim().ToLower(),
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };

            await _users.InsertOneAsync(user);

            return user.Id;
        }

        private async Task ValidateEmailAsync(string email)
        {
            email = email.Trim().ToLower();

            if (!MailAddress.TryCreate(email, out _))
            {
                throw new DomainValidationException($"Email address \"{email}\" is not valid!");
            }

            var user = await _users.FindAsync(user => user.Email.Equals(email));
            if (user.Any())
            {
                throw new DomainValidationException($"Email address \"{email}\" already exists!");
            }
        }

        private async Task ValidateUsernameAsync(string userName)
        {
            userName = userName.Trim().ToLower();
            var user = await _users.FindAsync(user => user.UserName.Equals(userName));
            if (user.Any())
            {
                throw new DomainValidationException($"Username \"{userName}\" already exists!");
            }
        }

        public async Task<bool> VerifyUserCredentialsAsync(UserCredential userCreds)
        {
            if (string.IsNullOrEmpty(userCreds.UserName))
            {
                throw new ArgumentException("Value cannot be empty or whitespace.", nameof(userCreds.UserName));
            }

            var userName = userCreds.UserName.Trim().ToLower();
            var query = await _users.FindAsync(user => user.UserName.Equals(userName));
            var user = query.FirstOrDefault();

            if (user == null)
            {
                throw new DomainNotFoundException($"User with username \"{userName}\" not found");
            }

            var passwordIsVerified = _encryptionServices.VerifyPasswordHash(
                userCreds.Password,
                user.PasswordHash,
                user.PasswordSalt);

            if (!passwordIsVerified)
            {
                return false;
            }

            return true;
        }

        public async Task<UserDto> GetUserAsync(string id)
        {
            var query = await _users.FindAsync(user => user.Id.Equals(id));
            User user = query.FirstOrDefault();

            if(user == null)
            {
                throw new DomainNotFoundException($"User with ID \"{id}\" does not exist.");
            }

            var userDto = new UserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName
            };

            return userDto;
        }
    }
}
