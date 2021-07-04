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

namespace UserManagement.Services
{
    public class UserServices : IUserServices
    {
        private readonly IMongoCollection<User> _users;

        public UserServices(IDbClient dbClient)
        {
            _users = dbClient.GetUserCollection();
        }

        public List<User> GetAllRegisteredUsers()
        {
            return _users.Find(user => true).ToList();
        }

        public async Task<string> RegisterUserAsync(UserRegistration userRegistration)
        {
            await ValidateUsernameAsync(userRegistration.UserName);
            await ValidateEmailAsync(userRegistration.Email);

            string encryptedPassword = EncryptPassword(userRegistration.Password);

            var user = new User
            {
                FirstName = userRegistration.FirstName.Trim().ToLower(),
                LastName = userRegistration.LastName.Trim().ToLower(),
                UserName = userRegistration.UserName.Trim().ToLower(),
                Password = encryptedPassword
            };

            _users.InsertOne(user);

            return user.Id;
        }

        private async Task ValidateEmailAsync(string email)
        {
            email = email.Trim().ToLower();

            if (!MailAddress.TryCreate(email, out _))
            {
                throw new DomainValidationException("email address is not valid!");
            }

            var user = await _users.FindAsync(user => user.Email.Equals(email));
            if (user.Any())
            {
                throw new DomainValidationException("email address already exists!");
            }
        }

        private async Task ValidateUsernameAsync(string userName)
        {
            userName = userName.Trim().ToLower();
            var user = await _users.FindAsync(user => user.UserName.Equals(userName));
            if (user.Any())
            {
                throw new DomainValidationException("username already exists!");
            }
        }

        private string EncryptPassword(string password)
        {
            throw new NotImplementedException();
        }

        public bool VerifyUserCredentials(UserCredential userCreds)
        {
            throw new NotImplementedException();
        }
    }
}
