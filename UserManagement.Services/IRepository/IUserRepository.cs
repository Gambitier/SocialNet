using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Services.Services.RequestModels;
using UserManagement.Services.Services.ResponseModels;

namespace UserManagement.Services.IRepository
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
