using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using UserManagement.Persistence.DataModels;
using UserManagement.Persistence.IRepository;
using UserManagement.Services.Exceptions;
using UserManagement.Services.Services;
using UserManagement.Services.Services.RequestModels;
using UserManagement.Services.Services.ResponseModels;

namespace UserManagement.Tests
{
    [TestClass]
    public class UserServicesTests
    {
        UserServices _sut;
        private readonly Mock<IUserRepository> _userRepository = new Mock<IUserRepository>();
        private readonly Mock<IEncryptionServices> _encryptionServices = new Mock<IEncryptionServices>();
        private readonly Mock<IEmailSender> emailSender = new Mock<IEmailSender>();

        public UserServicesTests()
        {
            _sut = new UserServices(
                _userRepository.Object,
                _encryptionServices.Object,
                emailSender.Object);
        }

        [TestMethod]
        public async Task VerifyUserCredentialsAsync_ShouldReturnUserDto_WhenUserExists()
        {
            //arrange
            const string UserName = "gambitier",
                         FirstName = "Akash",
                         LastName = "Jadhav",
                         Email = "akash@yopmail.com";

            UserCredential userCreds = new UserCredential
            {
                UserName = UserName,
                Password = "Test@123"
            };

            new EncryptionServices().CreatePasswordHash(
                userCreds.Password,
                out byte[] PasswordHash,
                out byte[] PasswordSalt);

            User user = new User()
            {
                Id = new Guid().ToString(),
                UserName = UserName,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                PasswordHash = PasswordHash,
                PasswordSalt = PasswordSalt
            };

            _userRepository.Setup(x => x.GetByUserName(UserName)).ReturnsAsync(user);
            _encryptionServices.Setup(x => 
                    x.VerifyPasswordHash(userCreds.Password, PasswordHash, PasswordSalt))
                .Returns(true);

            //act
            Tuple<bool, string> response = await _sut.VerifyUserCredentialsAsync(userCreds);
            bool isVerified = response.Item1;
            string userId = response.Item2;

            //assert
            Assert.IsTrue(isVerified);
            Assert.AreEqual(user.Id, userId);
        }


        [TestMethod]
        public async Task GetUserAsync_ShouldReturnUserDto_WhenUserExists()
        {
            //arrange
            string userId = new Guid().ToString();
            string UserName = "gambitier";
            string FirstName = "Akash";
            string LastName = "Jadhav";
            string Email = "akash@yopmail.com";

            User user = new User()
            {
                Id = userId,
                UserName = UserName,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email
            };

            _userRepository.Setup(x => x.GetByIdAsync(userId)).ReturnsAsync(user);

            //act
            UserDto response = await _sut.GetUserAsync(userId);

            //assert
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
            _userRepository.Setup(x => x.GetByIdAsync(userId)).ReturnsAsync((User)null);

            //assert
            Exception ex = await Assert.ThrowsExceptionAsync<DomainNotFoundException>(async () => 
                await _sut.GetUserAsync(userId)
            );

            Assert.AreEqual($"User with ID \"{userId}\" does not exist.", ex.Message);
        }
    }
}
