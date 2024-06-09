using Moq;
using WAMServer.Models;
using WAMServer.Interfaces;
using WAMServer.Controllers;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WAMServer.DTO;

namespace WAMServer.Tests.Controllers
{
    public class UsersControllerTests
    {
        private Mock<IRepository<User>> _mockUserRepository;
        private Mock<IRepository<Address>> _mockAddressRepository;

        public UsersControllerTests()
        {
            _mockUserRepository = new Mock<IRepository<User>>();
            _mockAddressRepository = new Mock<IRepository<Address>>();
        }

        public UsersController CreateUsersController()
        {
            return new UsersController(_mockUserRepository.Object, _mockAddressRepository.Object);
        }

        [Fact]
        public void GetUser_Returns_Unauthorized_When_User_Has_No_Id_Claim()
        {
            // Arrange
            var controller = CreateUsersController();
            var user = new ClaimsPrincipal();

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            // Act
            var result = controller.GetUser();

            // Assert
            Assert.IsType<UnauthorizedResult>(result.Result);
        }

        [Fact]
        public void GetUser_Returns_Unauthorized_When_User_Id_Cannot_Be_Parsed()
        {
            // Arrange
            var controller = CreateUsersController();
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("Id", "invalid")
            }));

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            // Act
            var result = controller.GetUser();

            // Assert
            Assert.IsType<UnauthorizedResult>(result.Result);
        }

        [Fact]
        public void GetUser_Returns_NotFound_When_User_Not_Found()
        {
            // Arrange
            var controller = CreateUsersController();
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("Id", "00000000-0000-0000-0000-000000000001")
            }));

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            _mockUserRepository.Setup(r => r.Get(It.IsAny<Guid>())).Returns((User)null);

            // Act
            var result = controller.GetUser();

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetUser_Returns_UserDTO_When_User_Found()
        {
            // Arrange
            var controller = CreateUsersController();
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("Id", "00000000-0000-0000-0000-000000000001")
            }));

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            var userEntity = new User
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                FirstName = "John",
                LastName = "Doe",
                AddressId = Guid.Parse("00000000-0000-0000-0000-000000000002")
            };

            var addressEntity = new Address
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                Street = "123 Main St",
                City = "Anytown",
                HouseNumber = "123",
                Zip = "12345"
            };

            _mockUserRepository.Setup(r => r.Get(It.IsAny<Guid>())).Returns(userEntity);
            _mockAddressRepository.Setup(r => r.Get(It.IsAny<Guid>())).Returns(addressEntity);

            // Act
            var result = controller.GetUser();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var userDTO = Assert.IsType<UserDTO>(okResult.Value);

            Assert.Equal("John", userDTO.FirstName);
            Assert.Equal("Doe", userDTO.LastName);
            Assert.NotNull(userDTO.Address);
            Assert.Equal("123 Main St", userDTO.Address.Street);
            Assert.Equal("Anytown", userDTO.Address.City);
            Assert.Equal("123", userDTO.Address.HouseNumber);
            Assert.Equal("12345", userDTO.Address.Zip);
        }
    }
}