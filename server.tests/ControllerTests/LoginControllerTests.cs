using Microsoft.Extensions.Configuration;
using Moq;
using WAMServer.Controllers;
using WAMServer.Interfaces;
using WAMServer.Models;

namespace WAMServer.Tests.Controllers
{
    public class LoginControllerTests
    {
        private Mock<IUserRepository> usersRepo;
        private Mock<IConfiguration> config;


        public LoginControllerTests()
        {
            usersRepo = new Mock<IUserRepository>();
            config = new Mock<IConfiguration>();
            // Setup mock
            setupMock();
        }

        [Fact]
        public void AuthenticateUserReturnNullIfUnableReturnsDesiredUser()
        {
            // Arrange
            User user = new User("John", "Doe", "john.doe@email.com", "supersecurepassword");
            usersRepo.Setup(x => x.GetUser(user.Email)).Returns(user);
            LoginController loginController = new LoginController(usersRepo.Object,  config.Object);
            // Act
            var result = loginController.AuthenticateUserReturnNullIfUnable(user.Email, user.Password);
            // Assert
            Assert.Equal(user, result);
        }

        // Util methods
        private void setupMock()
        {

        }
    }
}