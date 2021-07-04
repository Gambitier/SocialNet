using UserManagement.RequestModels;

namespace UserManagement.AuthManager
{
    public interface IJWTAuthenticationManager
    {
        string Authenticate(UserCredential userCreds);
    }
}
