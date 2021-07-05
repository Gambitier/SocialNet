using System.Threading.Tasks;
using UserManagement.RequestModels;

namespace UserManagement.AuthManager
{
    public interface IJWTAuthenticationManager
    {
        Task<string> AuthenticateAsync(UserCredential userCreds);
    }
}
