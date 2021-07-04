using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using UserManagement.DataModels;
using UserManagement.DBConfiguration;
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

        public string RegisterUser(UserRegistration userRegistration)
        {
            if (!UsernameIsUnique(userRegistration.UserName))
            {
                //throw error
            }

            if (!ValidateEmail(userRegistration.Email))
            {
                //throw error
            }

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

        private bool ValidateEmail(string email)
        {
            // validate email
            // check if email is unique
            throw new NotImplementedException();
        }

        private string EncryptPassword(string password)
        {
            throw new NotImplementedException();
        }

        private bool UsernameIsUnique(string userName)
        {
            throw new NotImplementedException();
        }

        public bool VerifyUserCredentials(UserCredential userCreds)
        {
            throw new NotImplementedException();
        }
    }
}
