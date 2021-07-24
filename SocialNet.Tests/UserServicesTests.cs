using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using SocialNet.Services.EmailService.Events;
using SocialNet.Services.Exceptions;
using SocialNet.Services.IRepository;
using SocialNet.Services.Services;
using SocialNet.Services.Services.RequestModels;
using SocialNet.Services.Services.ResponseModels;

namespace SocialNet.Tests
{
    [TestClass]
    public class UserServicesTests
    {
        UserServices _sut;
        private readonly Mock<IUserRepository> _userRepository = new();
        private readonly Mock<IEmailSender> emailSender = new();

        public UserServicesTests()
        {
            var emailevent = new UserRegisteredEmailEvent(emailSender.Object);
            _sut = new UserServices(
                _userRepository.Object,
                emailevent);
        }

        [TestMethod]
        public async Task RegisterUserAsync_ShouldAddUserAndReturnUserDto()
        {
            //arrange
            UserRegistration userRegistrationData = new()
            {
                UserName = "gambitier",
                FirstName = "Akash",
                LastName = "Jadhav",
                Password = "Test@123",
                Email = "akash@yopmail.com"
            };

            UserDto addUserResponse = new()
            {
                Id = new Guid().ToString(),
                UserName = userRegistrationData.UserName,
                FirstName = userRegistrationData.FirstName,
                LastName = userRegistrationData.LastName,
                Email = userRegistrationData.Email,
            };

            _userRepository.Setup(x => x.AddAsync(It.IsAny<UserRegistration>())).ReturnsAsync(addUserResponse);


            //act
            string registeredUserId = await _sut.RegisterUserAsync(userRegistrationData);

            //assert
            Assert.AreEqual(addUserResponse.Id, registeredUserId);
        }

        [TestMethod]
        public async Task VerifyUserCredentialsAsync_ShouldReturnUserDto_WhenUserExists()
        {
            //arrange
            string Id = new Guid().ToString();

            UserCredential userCreds = new()
            {
                UserName = "gambitier",
                Password = "Test@123"
            };

            var verificationResponse = new Tuple<bool, string>(true, Id);

            _userRepository.Setup(x => x.VerifyUserCredentials(It.IsAny<UserCredential>())).ReturnsAsync(verificationResponse);

            //act
            Tuple<bool, string> response = await _sut.VerifyUserCredentialsAsync(userCreds);

            //assert
            bool isVerified = response.Item1;
            string userId = response.Item2;
            Assert.IsTrue(isVerified);
            Assert.AreEqual(Id, userId);
        }

        [TestMethod]
        public async Task GetUserAsync_ShouldReturnUserDto_WhenUserExists()
        {
            //arrange
            string userId = new Guid().ToString();
            UserDto user = new()
            {
                Id = userId,
                UserName = "gambitier",
                FirstName = "Akash",
                LastName = "Jadhav",
                Email = "akash@yopmail.com"
            };

            _userRepository.Setup(x => x.GetByIdAsync(userId)).ReturnsAsync(user);

            //act
            UserDto response = await _sut.GetUserAsync(userId);

            //assert
            Assert.AreEqual(user.Id, response.Id);
            Assert.AreEqual(user.FirstName, response.FirstName);
            Assert.AreEqual(user.LastName, response.LastName);
            Assert.AreEqual(user.UserName, response.UserName);
            Assert.AreEqual(user.Email, response.Email);
        }

        [TestMethod]
        public async Task GetUserAsync_ShouldThrowException_WhenUserDoesNotExistsAsync()
        {
            //arrange
            string userId = new Guid().ToString();
            _userRepository.Setup(x => x.GetByIdAsync(userId)).ReturnsAsync((UserDto)null);

            //assert
            Exception ex = await Assert.ThrowsExceptionAsync<DomainNotFoundException>(async () =>
                await _sut.GetUserAsync(userId)
            );

            Assert.AreEqual($"User with ID \"{userId}\" does not exist.", ex.Message);
        }
    }
}
