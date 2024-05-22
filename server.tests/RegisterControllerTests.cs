

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using WAMServer.Controllers;
using WAMServer.Interfaces;
using WAMServer.Models;
using WAMServer.Records.Bodies;

namespace WAMServer.Tests.Controllers
{
    public class RegisterControllerTests
    {
        private readonly Mock<IRepository<User>> _userRepositoryMock;
        private readonly Mock<IEmailService> _mailServiceMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly RegisterController _controller;

        public RegisterControllerTests()
        {
            _configurationMock = new Mock<IConfiguration>();
            _userRepositoryMock = new Mock<IRepository<User>>();
            _mailServiceMock = new Mock<IEmailService>();

            _mailServiceMock.Setup(x => x.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            _controller = new RegisterController(_userRepositoryMock.Object, _mailServiceMock.Object, _configurationMock.Object);
        }

        [Fact]
        public async Task Post_Should_Return_BadRequest_When_Urls_Are_Null()
        {
            _configurationMock.Setup(x => x["Issuer"]).Returns("");
            _configurationMock.Setup(x => x["FrontendURL"]).Returns("");
            _mailServiceMock.Setup(x => x.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            // Arrange
            var userBody = new UserBody { Email = "test@example.com", Password = "Password123!", FirstName = "John", LastName = "Doe" };

            // Act
            var result = await _controller.Post(userBody);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult!.Value);
            Assert.Equal("Register.failed", (badRequestResult.Value as ErrorBody)!.Error);
        }

        [Fact]
        public async Task Post_Should_Return_BadRequest_When_Email_Is_Already_Taken()
        {
            // Arrange
            _configurationMock.Setup(x => x["JWT:Issuer"]).Returns("http://localhost:5000");
            _mailServiceMock.Setup(x => x.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            _configurationMock.Setup(x => x["FrontendURL"]).Returns("http://localhost:3000");
            var userBody = new UserBody { Email = "test@example.com", Password = "Password123!", FirstName = "John", LastName = "Doe" };
            _userRepositoryMock.Setup(x => x.GetAll(It.IsAny<Func<User, bool>>())).Returns(new List<User> { new User(userBody.FirstName, userBody.LastName, userBody.Email, BCrypt.Net.BCrypt.EnhancedHashPassword(userBody.Password)) }.AsQueryable());

            // Act
            var result = await _controller.Post(userBody);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult!.Value);
            Assert.Equal("Register.emailtaken", (badRequestResult.Value as ErrorBody)!.Error);
        }

        [Fact]
        public async Task Post_Should_Return_BadRequest_For_Invalid_Email()
        {
            // Arrange
            _configurationMock.Setup(x => x["JWT:Issuer"]).Returns("http://localhost:5000");
            _configurationMock.Setup(x => x["FrontendURL"]).Returns("http://localhost:3000");
            _mailServiceMock.Setup(x => x.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            var userBody = new UserBody { Email = "invalid-email", Password = "Password123!", FirstName = "John", LastName = "Doe" };

            // Act
            var result = await _controller.Post(userBody);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult!.Value);
            Assert.Equal("Wrong approach, incident reported.", (badRequestResult.Value as ErrorBody)!.Error);
        }

        [Fact]
        public async Task Post_Should_Return_BadRequest_For_Invalid_Password()
        {
            // Arrange
            _configurationMock.Setup(x => x["JWT:Issuer"]).Returns("http://localhost:5000");
            _configurationMock.Setup(x => x["FrontendURL"]).Returns("http://localhost:3000");
            _mailServiceMock.Setup(x => x.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            var userBody = new UserBody { Email = "test@example.com", Password = "short", FirstName = "John", LastName = "Doe" };

            // Act
            var result = await _controller.Post(userBody);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult!.Value);
            Assert.Equal("Wrong approach, incident reported.", (badRequestResult.Value as ErrorBody)!.Error);
        }

        [Fact]
        public async Task Post_Should_Return_Ok_When_User_Is_Registered()
        {
            // Arrange
            _configurationMock.Setup(x => x["JWT:Issuer"]).Returns("http://localhost:5000");
            _configurationMock.Setup(x => x["FrontendURL"]).Returns("http://localhost:3000");
            _mailServiceMock.Setup(x => x.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            var userBody = new UserBody { Email = "test@example.com", Password = "Password123!", FirstName = "John", LastName = "Doe" };
            var user = new User(userBody.FirstName, userBody.LastName, userBody.Email, userBody.Password);
            _userRepositoryMock.Setup(x => x.GetAll(It.IsAny<Func<User, bool>>())).Returns(new List<User>().AsQueryable());
            _userRepositoryMock.Setup(x => x.AddAsync(It.IsAny<User>())).ReturnsAsync(user);

            // Act
            var result = await _controller.Post(userBody);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Confirm_Should_Return_BadRequest_For_Invalid_UserId()
        {
            // Arrange
            _configurationMock.Setup(x => x["JWT:Issuer"]).Returns("http://localhost:5000");
            _configurationMock.Setup(x => x["FrontendURL"]).Returns("http://localhost:3000");
            _mailServiceMock.Setup(x => x.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            // Act
            var result = await _controller.Confirm("invalid-user-id", "some-token");

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Confirm_Should_Return_NotFound_When_User_Not_Exists()
        {
            // Arrange
            _configurationMock.Setup(x => x["JWT:Issuer"]).Returns("http://localhost:5000");
            _configurationMock.Setup(x => x["FrontendURL"]).Returns("http://localhost:3000");
            _userRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns<User?>(null!);
            _mailServiceMock.Setup(x => x.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            // Act
            var result = await _controller.Confirm(Guid.NewGuid().ToString(), "some-token");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Confirm_Should_Return_BadRequest_For_Invalid_Token()
        {
            // Arrange
            _configurationMock.Setup(x => x["JWT:Issuer"]).Returns("http://localhost:5000");
            _configurationMock.Setup(x => x["FrontendURL"]).Returns("http://localhost:3000");
            var user = new User { ConfirmationToken = Guid.NewGuid() };
            _userRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(user);
            _mailServiceMock.Setup(x => x.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            // Act
            var result = await _controller.Confirm(user.Id.ToString(), "invalid-token");

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Confirm_Should_Return_Ok_When_User_Is_Confirmed()
        {
            // Arrange
            _configurationMock.Setup(x => x["JWT:Issuer"]).Returns("http://localhost:5000");
            _configurationMock.Setup(x => x["FrontendURL"]).Returns("http://localhost:3000");
            var user = new User { Id = Guid.NewGuid(), ConfirmationToken = Guid.NewGuid() };
            _userRepositoryMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(user);
            _userRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<User>(), It.IsAny<Func<User, bool>>())).ReturnsAsync(new User());
            _mailServiceMock.Setup(x => x.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            // Act
            var result = await _controller.Confirm(user.Id.ToString(), user.ConfirmationToken.ToString());

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult!.Value);
            Assert.Equal("http://localhost:3000", okResult!.Value);
        }
    }
}