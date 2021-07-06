using System.Threading.Tasks;
using UserManagement.Services.Services.RequestModels;

namespace UserManagement.AuthManager
{
    public interface IJWTAuthenticationManager
    {
        Task<string> AuthenticateAsync(UserCredential userCreds);
    }
}
