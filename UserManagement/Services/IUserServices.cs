using System;
using System.Threading.Tasks;
using UserManagement.RequestModels;
using UserManagement.ResponseModels;

namespace UserManagement.Services
{
    public interface IUserServices
    {
        /// <summary>
        /// Receives user information and saves information to DB and returns UserID
        /// </summary>
        /// <param name="userRegistration"></param>
        /// <returns>string: UserId</returns>
        Task<string> RegisterUserAsync(UserRegistration userRegistration);

        /// <summary>
        /// verifies username and password
        /// </summary>
        /// <param name="userCreds"></param>
        /// <returns>bool: userIsVerified & string: userID</returns>
        Task<Tuple<bool, string>> VerifyUserCredentialsAsync(UserCredential userCreds);

        /// <summary>
        /// get registered user's details
        /// </summary>
        /// <returns>UserDto: user</returns>
        Task<UserDto> GetUserAsync(string id);
    }
}
