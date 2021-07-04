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
            throw new NotImplementedException();
        }

        public bool VerifyUserCredentials(UserCredential userCreds)
        {
            throw new NotImplementedException();
        }
    }
}
