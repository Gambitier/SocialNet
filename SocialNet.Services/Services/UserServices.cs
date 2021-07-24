using System;
using System.Net.Mail;
using System.Threading.Tasks;
using SocialNet.Services.EmailService.EmailEventArgs;
using SocialNet.Services.EmailService.Events;
using SocialNet.Services.Exceptions;
using SocialNet.Services.IRepository;
using SocialNet.Services.Services.RequestModels;
using SocialNet.Services.Services.ResponseModels;

namespace SocialNet.Services.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;
        private event EventHandler<UserRegisteredEventArgs> UserRegisteredEventHandler;

        public UserServices(
            IUserRepository userRepository,
            UserRegisteredEmailEvent userRegisteredEmailEvent)
        {
            _userRepository = userRepository;
            UserRegisteredEventHandler += userRegisteredEmailEvent.SendEmail;
        }

        public async Task<string> RegisterUserAsync(UserRegistration userRegistration)
        {
            await ValidateUsernameAsync(userRegistration.UserName);
            await ValidateEmailAsync(userRegistration.Email);

            UserDto addedUser = await _userRepository.AddAsync(userRegistration);

            UserRegisteredEventHandler?
                .Invoke(this, new UserRegisteredEventArgs(userRegistration));

            return addedUser.Id;
        }

        private async Task ValidateEmailAsync(string email)
        {
            if (!MailAddress.TryCreate(email, out _))
            {
                throw new DomainValidationException($"Email address \"{email}\" is not valid!");
            }

            var user = await _userRepository.GetByEmail(email);
            if (user != null)
            {
                throw new DomainValidationException($"Email address \"{email}\" already exists!");
            }
        }

        private async Task ValidateUsernameAsync(string userName)
        {
            UserDto user = await _userRepository.GetByUsernameAsync(userName);
            if (user != null)
            {
                throw new DomainValidationException($"Username \"{userName}\" already exists!");
            }
        }

        public async Task<Tuple<bool,string>> VerifyUserCredentialsAsync(UserCredential userCreds)
        {
            if (string.IsNullOrEmpty(userCreds.UserName))
                throw new ArgumentException("Value cannot be empty or whitespace.", nameof(userCreds.UserName));

            if (string.IsNullOrEmpty(userCreds.Password))
                throw new ArgumentException("Value cannot be empty or whitespace.", nameof(userCreds.Password));

            var response = await _userRepository.VerifyUserCredentials(userCreds);
            bool passwordIsVerified = response.Item1;

            if (!passwordIsVerified)
            {
                throw new DomainValidationException("Username or password is incorrect");
            }

            return response;
        }

        public async Task<UserDto> GetUserAsync(string id)
        {
            UserDto userDto = await _userRepository.GetByIdAsync(id);

            if(userDto == null)
            {
                throw new DomainNotFoundException($"User with ID \"{id}\" does not exist.");
            }

            return userDto;
        }
    }
}
