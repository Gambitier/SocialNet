using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using UserManagement.Persistence.DataModels;
using UserManagement.Persistence.IRepository;
using UserManagement.Services.Exceptions;
using UserManagement.Services.Services;
using UserManagement.Services.Services.ResponseModels;

namespace UserManagement.Tests
{
    [TestClass]
    public class UserServicesTests
    {
        UserServices _sut;
        private readonly Mock<IUserRepository> _userRepository = new Mock<IUserRepository>();
        private readonly Mock<IEncryptionServices> encryptionServices = new Mock<IEncryptionServices>();
        private readonly Mock<IEmailSender> emailSender = new Mock<IEmailSender>();

        public UserServicesTests()
        {
            _sut = new UserServices(
                _userRepository.Object,
                encryptionServices.Object,
                emailSender.Object);
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
