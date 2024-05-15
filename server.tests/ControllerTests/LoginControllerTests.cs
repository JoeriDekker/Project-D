using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Moq;
using WAMServer.Controllers;
using WAMServer.Interfaces;
using WAMServer.Models;

namespace WAMServer.Tests.Controllers
{
    public class LoginControllerTests
    {
        private Mock<ILoginService> loginService;
        private Mock<IConfiguration> config;
        private Mock<IConfiguration> configMock;
        public LoginControllerTests()
        {
            loginService = new Mock<ILoginService>();
            config = new Mock<IConfiguration>();
            configMock = new Mock<IConfiguration>();
            // Setup mock configuration
            configMock.Setup(x => x["Jwt:Key"]).Returns("ThisIsTheTestKeyItNeedsToBeQuiteLongThatsWhyItsSoLong");
            configMock.Setup(x => x["Jwt:Issuer"]).Returns("testIssuer");
        }

        [Fact]
        public void AuthenticateUserReturnNullIfUnableReturnsDesiredUser()
        {
            // Arrange
            User user = new User("John", "Doe", "john.doe@email.com", "supersecurepassword");
            loginService.Setup(x => x.GetUser(user.Email)).Returns(user);
            LoginController loginController = new LoginController(loginService.Object, config.Object);
            // Act
            var result = loginController.AuthenticateUserReturnNullIfUnable(user.Email, user.Password);
            // Assert
            Assert.Equal(user, result);
        }

        [Fact]
        public void AuthenticateUserReturnNullIfUnableReturnsNull()
        {
            // Arrange
            // Arranges a user with a different email and password
            User user = new User("John", "Doe", "john.doe@email.com", "supersecurepassword");
            // Sets up the login service to return the user
            loginService.Setup(x => x.GetUser(user.Email)).Returns(user);
            LoginController loginController = new(loginService.Object, config.Object);
            // Act
            var result = loginController.AuthenticateUserReturnNullIfUnable("wrongemail@mail.com", "totallywrongpassword");
            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GenerateJSONWebToken_ReturnsValidToken()
        {
            // Arrange
            var userInfo = new User("John", "Doe", "john.doe@email.com", "supersecurepassword");
            var controller = new LoginController(null!, configMock.Object);

            // Act
            var token = controller.GenerateJSONWebToken(userInfo);

            // Assert
            Assert.NotNull(token);
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            Assert.Equal("testIssuer", jwtToken.Issuer);
            Assert.Equal(DateTime.Now.AddMinutes(120).Date, jwtToken.ValidTo.Date); // Check expiry
            Assert.Contains(jwtToken.Claims, claim => claim.Type == JwtRegisteredClaimNames.Email && claim.Value == userInfo.Email);
            Assert.Contains(jwtToken.Claims, claim => claim.Type == "Id" && claim.Value == userInfo.Id.ToString());
        }

        [Fact]
        public void GenerateJSONWebToken_ReturnsTokenWithDefaultKeyWhenConfigKeyIsNull()
        {
            // Arrange
            var userInfo = new User("John", "Doe", "john.doe@email.com", "supersecurepassword");
            configMock.Setup(x => x["Jwt:Key"]).Returns((string)null!); // Simulating null configuration key
            var controller = new LoginController(null!, configMock.Object);

            // Act
            var token = controller.GenerateJSONWebToken(userInfo);

            // Assert
            Assert.NotNull(token);
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            Assert.Equal("testIssuer", jwtToken.Issuer);
            Assert.Equal(DateTime.Now.AddMinutes(120).Date, jwtToken.ValidTo.Date); // Check expiry
            Assert.Contains(jwtToken.Claims, claim => claim.Type == JwtRegisteredClaimNames.Email && claim.Value == userInfo.Email);
            Assert.Contains(jwtToken.Claims, claim => claim.Type == "Id" && claim.Value == userInfo.Id.ToString());
        }
    }
}