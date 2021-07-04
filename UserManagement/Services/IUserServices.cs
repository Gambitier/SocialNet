using System.Collections.Generic;
using UserManagement.DataModels;
using UserManagement.RequestModels;

namespace UserManagement.Services
{
    public interface IUserServices
    {
        /// <summary>
        /// Receives user information and saves information to DB and returns UserID
        /// </summary>
        /// <param name="userRegistration"></param>
        /// <returns>string: UserId</returns>
        string RegisterUser(UserRegistration userRegistration);

        /// <summary>
        /// verifies username and password
        /// </summary>
        /// <param name="userCreds"></param>
        /// <returns>bool: userIsVerified</returns>
        bool VerifyUserCredentials(UserCredential userCreds);

        /// <summary>
        /// get all registered user's list
        /// </summary>
        /// <returns>List<User>: userList</returns>
        List<User> GetAllRegisteredUsers();
    }
}
