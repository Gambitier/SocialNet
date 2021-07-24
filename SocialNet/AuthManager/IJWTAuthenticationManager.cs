using SocialNet.Services.Services.RequestModels;
using System.Threading.Tasks;

namespace SocialNet.AuthManager
{
    public interface IJWTAuthenticationManager
    {
        Task<string> AuthenticateAsync(UserCredential userCreds);
    }
}
