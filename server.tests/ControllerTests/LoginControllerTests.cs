using Microsoft.Extensions.Configuration;
using Moq;
using WAMServer.Controllers;
using WAMServer.Interfaces;
using WAMServer.Models;
using Xunit;

namespace WAMServer.Tests.Controllers
{
    public class LoginControllerTests
    {
        private Mock<ILoginService> loginService;
        private Mock<IConfiguration> config;


        public LoginControllerTests()
        {
            loginService = new Mock<ILoginService>();
            config = new Mock<IConfiguration>();
            // Setup mock
            setupMock();
        }

        [Fact]
        public void AuthenticateUserReturnNullIfUnableReturnsDesiredUser()
        {
            // Arrange
            User user = new User("John", "Doe", "john.doe@email.com", "supersecurepassword");
            loginService.Setup(x => x.GetUser(user.Email)).Returns(user);
            LoginController loginController = new LoginController(loginService.Object,  config.Object);
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