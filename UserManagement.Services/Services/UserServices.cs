using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Net.Mail;
using System.Threading.Tasks;
using UserManagement.Persistence.DataModels;
using UserManagement.Persistence.IRepository;
using UserManagement.Services.Exceptions;
using UserManagement.Services.Services.RequestModels;
using UserManagement.Services.Services.ResponseModels;

namespace UserManagement.Services.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncryptionServices _encryptionServices;
        private readonly IEmailSender _emailSender;

        public UserServices(
            IUserRepository userRepository,
            IEncryptionServices encryptionServices,
            IEmailSender emailSender)
        {
            _userRepository = userRepository;
            _encryptionServices = encryptionServices;
            _emailSender = emailSender;
        }

        public async Task<string> RegisterUserAsync(UserRegistration userRegistration)
        {
            await ValidateUsernameAsync(userRegistration.UserName);
            await ValidateEmailAsync(userRegistration.Email);

            _encryptionServices.CreatePasswordHash(
                userRegistration.Password,
                out byte[] passwordHash,
                out byte[] passwordSalt);

            var user = new User
            {
                FirstName = userRegistration.FirstName.Trim().ToLower(),
                LastName = userRegistration.LastName.Trim().ToLower(),
                UserName = userRegistration.UserName.Trim().ToLower(),
                Email = userRegistration.Email.Trim().ToLower(),
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };

            await _userRepository.Add(user);

            await _emailSender.SendEmailAsync(
                userRegistration.Email,
                "welcome to usermanagement",
                $"Welcome!! <br/><br/> " +
                $"your username is \"{userRegistration.UserName.Trim().ToLower()}\" " +
                $"and password is \"{userRegistration.Password}\"");

            return user.Id;
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
            User user = await _userRepository.GetByUserName(userName);
            if (user != null)
            {
                throw new DomainValidationException($"Username \"{userName}\" already exists!");
            }
        }

        public async Task<Tuple<bool,string>> VerifyUserCredentialsAsync(UserCredential userCreds)
        {
            if (string.IsNullOrEmpty(userCreds.UserName))
            {
                throw new ArgumentException("Value cannot be empty or whitespace.", nameof(userCreds.UserName));
            }

            string userName = userCreds.UserName.Trim().ToLower();
            User user = await _userRepository.GetByUserName(userName);
            if (user == null)
            {
                throw new DomainNotFoundException($"User with username \"{userName}\" not found");
            }

            var passwordIsVerified = _encryptionServices.VerifyPasswordHash(
                userCreds.Password,
                user.PasswordHash,
                user.PasswordSalt);

            if (!passwordIsVerified)
            {
                return new Tuple<bool, string>(false, string.Empty);
            }

            return new Tuple<bool, string>(true, user.Id);
        }

        public async Task<UserDto> GetUserAsync(string id)
        {
            var user = await _userRepository.GetById(id);

            if(user == null)
            {
                throw new DomainNotFoundException($"User with ID \"{id}\" does not exist.");
            }

            var userDto = new UserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName
            };

            return userDto;
        }
    }
}
