using SocialNet.Services.Services.RequestModels;

namespace SocialNet.Services.EmailService.EmailEventArgs
{
    public class UserRegisteredEventArgs : BaseEmailEventArgs
    {
        public UserRegistration RegisteredUserData { get; private set; }

        public UserRegisteredEventArgs(UserRegistration registeredUserData)
        {
            RegisteredUserData = registeredUserData;
        }
    }
}
