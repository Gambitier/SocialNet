using System;
using System.Threading.Tasks;
using SocialNet.Services.Services.RequestModels;
using SocialNet.Services.Services.ResponseModels;

namespace SocialNet.Services.IRepository
{
    public interface IUserRepository
    {
        Task<UserDto> GetByEmail(string email);
        Task<Tuple<bool, string>> VerifyUserCredentials(UserCredential userCreds);
        Task<UserDto> GetByIdAsync(string id);
        Task<UserDto> GetByUsernameAsync(string username);
        Task<UserDto> AddAsync(UserRegistration entity);
    }
}
