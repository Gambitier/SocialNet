using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Net.Mail;
using System.Threading.Tasks;
using UserManagement.Services.EmailService.EmailEventArgs;
using UserManagement.Services.EmailService.Events;
using UserManagement.Services.Exceptions;
using UserManagement.Services.IRepository;
using UserManagement.Services.Services.RequestModels;
using UserManagement.Services.Services.ResponseModels;

namespace UserManagement.Services.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;

        public event EventHandler<UserRegisteredEventArgs> UserRegisteredEventHandler;

        public UserServices(
            IUserRepository userRepository,
            IEmailSender emailSender)
        {
            _userRepository = userRepository;

            // todo : should we instantiate like this?
            var emailEvents = new UserRegistered(emailSender);
            UserRegisteredEventHandler += emailEvents.UserRegisteredEvent;
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
            email = email.Trim().ToLower();

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
            userName = userName.Trim().ToLower();
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
