using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using WAMServer.Controllers;
using WAMServer.Interfaces;
using WAMServer.Models;
using WAMServer.Records;

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
            string password = "supersecurepassword";
            User user = new User("John", "Doe", "john.doe@email.com", BCrypt.Net.BCrypt.EnhancedHashPassword(password));
            loginService.Setup(x => x.GetUser(user.Email)).Returns(user);
            LoginController loginController = new LoginController(loginService.Object, config.Object);
            // Act
            var result = loginController.AuthenticateUserReturnNullIfUnable(user.Email, password);
            // Assert
            Assert.Equal(user, result);
        }

        [Fact]
        public void AuthenticateUserReturnNullIfUnableReturnsNull()
        {
            // Arrange
            // Arranges a user with a different email and password
            string password = "supersecurepassword";
            User user = new User("John", "Doe", "john.doe@email.com", BCrypt.Net.BCrypt.EnhancedHashPassword(password));
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
            var userInfo = new User("John", "Doe", "john.doe@email.com", BCrypt.Net.BCrypt.EnhancedHashPassword("supersecurepassword"));
            var controller = new LoginController(null!, configMock.Object);

            // Act
            var token = controller.GenerateJSONWebToken(userInfo);

            // Assert
            Assert.NotNull(token);
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            Assert.Equal("testIssuer", jwtToken.Issuer);
            Assert.Equal(DateTime.Now.AddMinutes(120).ToUniversalTime().Date, jwtToken.ValidTo.Date); // Check expiry
            Assert.Contains(jwtToken.Claims, claim => claim.Type == JwtRegisteredClaimNames.Email && claim.Value == userInfo.Email);
            Assert.Contains(jwtToken.Claims, claim => claim.Type == "Id" && claim.Value == userInfo.Id.ToString());
        }

        [Fact]
        public void GenerateJSONWebToken_ReturnsTokenWithDefaultKeyWhenConfigKeyIsNull()
        {
            // Arrange
            var userInfo = new User("John", "Doe", "john.doe@email.com", BCrypt.Net.BCrypt.EnhancedHashPassword("supersecurepassword"));
            configMock.Setup(x => x["Jwt:Key"]).Returns((string)null!); // Simulating null configuration key
            var controller = new LoginController(null!, configMock.Object);

            // Act
            var token = controller.GenerateJSONWebToken(userInfo);

            // Assert
            Assert.NotNull(token);
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            Assert.Equal("testIssuer", jwtToken.Issuer);
            Assert.Equal(DateTime.Now.AddMinutes(120).ToUniversalTime().Date, jwtToken.ValidTo.Date); // Check expiry
            Assert.Contains(jwtToken.Claims, claim => claim.Type == JwtRegisteredClaimNames.Email && claim.Value == userInfo.Email);
            Assert.Contains(jwtToken.Claims, claim => claim.Type == "Id" && claim.Value == userInfo.Id.ToString());
        }

        [Theory]
        [InlineData("")]
        [InlineData("aa")]
        [InlineData("aaaaaa")]
        [InlineData("a@a.a")]
        public void Login_Returns_Unauthorized_When_Email_Invalid(string email)
        {
            // Arrange
            var loginBody = new LoginBody(email, "password");
            var controller = new LoginController(null!, config.Object);
            var expected = "The email address and/or the password did not match.";

            // Act
            var result = controller.Login(loginBody);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal(expected, unauthorizedResult.Value);
        }

        [Fact]
        public void Login_Returns_Unauthorized_When_User_Not_Found()
        {
            // Arrange
            var loginBody = new LoginBody("email@email.com", "password");
            var controller = new LoginController(loginService.Object, config.Object);
            var expected = "The email address and/or the password did not match.";
            loginService.Setup(x => x.GetUser(loginBody.Email)).Returns((User)null!);

            // Act
            var result = controller.Login(loginBody);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal(expected, unauthorizedResult.Value);
        }

        [Fact]
        public void Login_Returns_Unauthorized_When_User_Not_Confirmed()
        {
            // Arrange
            var loginBody = new LoginBody("email@email.com", "password");
            var controller = new LoginController(loginService.Object, config.Object);
            var expected = "The email address and/or the password did not match.";

            var user = new User("John", "Doe", loginBody.Email, BCrypt.Net.BCrypt.EnhancedHashPassword(loginBody.Password));
            loginService.Setup(x => x.GetUser(loginBody.Email)).Returns(user);

            // Act
            var result = controller.Login(loginBody);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal(expected, unauthorizedResult.Value);
        }

        [Fact]
        public void Login_Returns_Ok_When_User_Found_And_Confirmed()
        {
            // Arrange
            var loginBody = new LoginBody("email@email.com", "password");
            var controller = new LoginController(loginService.Object, config.Object);

            var user = new User("John", "Doe", loginBody.Email, BCrypt.Net.BCrypt.EnhancedHashPassword(loginBody.Password));
            user.IsConfirmed = true;
            loginService.Setup(x => x.GetUser(loginBody.Email)).Returns(user);

            // Act
            var result = controller.Login(loginBody);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }
    }
}