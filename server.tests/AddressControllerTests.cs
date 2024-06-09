using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Org.BouncyCastle.Asn1.Nist;
using WAMServer.Controllers;
using WAMServer.Interfaces;
using WAMServer.Models;
using WAMServer.Records.Bodies;

namespace WAMServer.Tests.Controllers
{
    public class AddressControllerTests
    {
        private readonly Mock<IRepository<Address>> _mockAddressRepository;
        private readonly Mock<IRepository<User>> _mockUserRepository;
        
        public AddressControllerTests()
        {
            _mockAddressRepository = new Mock<IRepository<Address>>();
            _mockUserRepository = new Mock<IRepository<User>>();
        }

        public AddressController CreateAddressController()
        {
            return new AddressController(_mockAddressRepository.Object, _mockUserRepository.Object);
        }

        [Fact]
        public void Put_Returns_Unauthorized_When_No_Id_Claim()
        {
            // Arrange
            var controller = CreateAddressController();
            var addressPatchBody = new AddressPatchBody();
            var currentUser = new ClaimsPrincipal();
            var unauthorizedmsg = "Errors.unauth";
            var expected = new ErrorBody(unauthorizedmsg);
            var expectedStatusCode = 401;
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = currentUser
                }
            };

            // Act
            var result = controller.Put(addressPatchBody);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            var actual = (ErrorBody)unauthorizedResult.Value!;
            Assert.Equal(expected, actual);
            Assert.Equal(expectedStatusCode, unauthorizedResult.StatusCode);
        }

        [Fact]
        public void Put_Returns_Unauthorized_When_UserId_Not_Guid()
        {
            // Arrange
            var controller = CreateAddressController();
            var addressPatchBody = new AddressPatchBody();
            var currentUser = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("Id", "notaguid")
            }));
            var unauthorizedmsg = "Errors.unauth";
            var expected = new ErrorBody(unauthorizedmsg);
            var expectedStatusCode = 401;
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = currentUser
                }
            };

            // Act
            var result = controller.Put(addressPatchBody);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            var actual = (ErrorBody)unauthorizedResult.Value!;
            Assert.Equal(expected, actual);
            Assert.Equal(expectedStatusCode, unauthorizedResult.StatusCode);
        }

        [Fact]
        public void Put_Returns_Unauthorized_When_User_Not_Found()
        {
            // Arrange
            var controller = CreateAddressController();
            var addressPatchBody = new AddressPatchBody();
            var currentUser = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("Id", Guid.NewGuid().ToString())
            }));
            var unauthorizedmsg = "Errors.unauth";
            var expected = new ErrorBody(unauthorizedmsg);
            var expectedStatusCode = 401;
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = currentUser
                }
            };

            // Act
            var result = controller.Put(addressPatchBody);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            var actual = (ErrorBody)unauthorizedResult.Value!;
            Assert.Equal(expected, actual);
            Assert.Equal(expectedStatusCode, unauthorizedResult.StatusCode);
        }

        [Fact]
        public void Put_Returns_Unauthorized_When_Password_Incorrect()
        {
            // Arrange
            var controller = CreateAddressController();
            var addressPatchBody = new AddressPatchBody()
            {
                Password = "TotallyIncorrectPassword",
                Street = "1234 Main St",
                City = "Springfield",
                HouseNumber = "1234",
            };
            var currentUser = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("Id", Guid.NewGuid().ToString())
            }));
            var passincorrectmsg = "Errors.incorrectpassword";
            var expected = new ErrorBody(passincorrectmsg);
            var expectedStatusCode = 401;
            var user = new User
            {
                Password = BCrypt.Net.BCrypt.EnhancedHashPassword("password")
            };
            _mockUserRepository.Setup(x => x.Get(It.IsAny<Guid>())).Returns(user);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = currentUser
                }
            };

            // Act
            var result = controller.Put(addressPatchBody);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            var actual = (ErrorBody)unauthorizedResult.Value!;
            Assert.Equal(expected, actual);
            Assert.Equal(expectedStatusCode, unauthorizedResult.StatusCode);
        }

        [Fact]
        public void Put_Returns_NoContent()
        {
            // Arrange
            var controller = CreateAddressController();
            var addressPatchBody = new AddressPatchBody
            {
                Password = "password"
            };
            var currentUser = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("Id", Guid.NewGuid().ToString())
            }));
            var user = new User
            {
                Password = BCrypt.Net.BCrypt.EnhancedHashPassword("password")
            };
            _mockUserRepository.Setup(x => x.Get(It.IsAny<Guid>())).Returns(user);
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = currentUser
                }
            };

            // Act
            var result = controller.Put(addressPatchBody);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        
    }
}