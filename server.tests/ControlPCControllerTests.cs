using System.Linq.Expressions;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WAMServer.Controllers;
using WAMServer.Interfaces;
using WAMServer.Models;

namespace WAMServer.Tests.Controllers
{
    public class ControlPCControllerTests
    {
        private Mock<IRepository<ControlPC>> _controlPCRepository;
        private Mock<IRepository<User>> _userRepository;
        private Mock<IControlPC<ControlPC>> _controlPCService;

        public ControlPCControllerTests()
        {
            _controlPCRepository = new Mock<IRepository<ControlPC>>();
            _userRepository = new Mock<IRepository<User>>();
            _controlPCService = new Mock<IControlPC<ControlPC>>();
        }

        public ControlPCController CreateControlPCController()
        {
            return new ControlPCController(_controlPCRepository.Object, _userRepository.Object, _controlPCService.Object);
        }

        [Fact]
        public async Task GetControlPCs_Returns_Empty_List()
        {
            // Arrange
            var controlPCController = CreateControlPCController();

            // Act
            var result = await controlPCController.GetControlPCs();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var controlpcs = Assert.IsAssignableFrom<IEnumerable<ControlPC>>(okResult.Value);
            Assert.NotNull(controlpcs);
            Assert.Empty(controlpcs);
        }

        [Fact]
        public async Task GetControlPCs_Returns_Desired_List()
        {
            // Arrange
            var controlPCs = new List<ControlPC>
            {
                new ControlPC(Guid.NewGuid(), "Secret1", "BRO-1", "Secret controlpc1"),
                new ControlPC(Guid.NewGuid(), "Secret2", "BRO-2", "Secret controlpc2")
            };
            _controlPCRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(controlPCs);

            var controlPCController = CreateControlPCController();

            // Act
            var result = await controlPCController.GetControlPCs();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var controlpcs = Assert.IsAssignableFrom<IEnumerable<ControlPC>>(okResult.Value);
            Assert.NotNull(controlpcs);
            Assert.Equal(2, controlpcs.Count());
        }

        [Fact]
        public async Task GetControlPC_Returns_NotFound()
        {
            // Arrange
            var controlPCController = CreateControlPCController();

            // Act
            var result = await controlPCController.GetControlPC(Guid.NewGuid());

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetControlPC_Returns_Desired_ControlPC()
        {
            // Arrange
            var expectedControlPC = new ControlPC(Guid.NewGuid(), "Secret1", "BRO-1", "Secret controlpc1");
            _controlPCService.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(expectedControlPC);

            var controlPCController = CreateControlPCController();

            // Act
            var result = await controlPCController.GetControlPC(expectedControlPC.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var controlpc = Assert.IsAssignableFrom<ControlPC>(okResult.Value);
            Assert.NotNull(controlpc);
            Assert.Equal(expectedControlPC.Id, controlpc.Id);
            Assert.Equal(expectedControlPC.userId, controlpc.userId);
            Assert.Equal(expectedControlPC.secret, controlpc.secret);
            Assert.Equal(expectedControlPC.meetputBroID, controlpc.meetputBroID);
            Assert.Equal(expectedControlPC.ControlPCSecret, controlpc.ControlPCSecret);
        }

        [Fact]
        public async Task CreateControlPC_Returns_BadRequest_When_Invalid_ControlPC_Is_Passed()
        {
            // Arrange
            var controlPCController = CreateControlPCController();

            // Act
            var result = await controlPCController.CreateControlPC(null!);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task CreateControlPC_Returns_Unauthorized_When_User_Is_Not_Authorized()
        {
            // Arrange
            var controlPCController = CreateControlPCController();
            controlPCController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity())
                }
            };

            // Act
            var result = await controlPCController.CreateControlPC(new ControlPC(
                Guid.NewGuid(),
                "Secret",
                "BRO-1",
                "Secret controlpc"
            ));

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result.Result);
        }

        [Fact]
        public async Task CreateControlPC_Returns_Unauthorized_When_Invalid_UserID_Is_Passed()
        {
            // Arrange
            var controlPCController = CreateControlPCController();
            controlPCController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                    {
                        new Claim("Id", "invalid")
                    }))
                }
            };

            // Act
            var result = await controlPCController.CreateControlPC(new ControlPC(
                Guid.NewGuid(),
                "Secret",
                "BRO-1",
                "Secret controlpc"
            ));

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result.Result);
        }

        [Fact]
        public async Task CreateControlPC_Returns_Created_ControlPC()
        {
            // Arrange
            var expectedControlPC = new ControlPC(
                Guid.NewGuid(),
                "Secret",
                "BRO-1",
                "Secret controlpc"
            );
            // _controlPCService.Setup(x => x.CreateAsync(It.IsAny<ControlPC>())).Returns(expectedControlPC);

            var controlPCController = CreateControlPCController();

            controlPCController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                    {
                        new Claim("Id", Guid.NewGuid().ToString())
                    }))
                }
            };

            // Act
            var result = await controlPCController.CreateControlPC(expectedControlPC);

            // Assert
            var okResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var controlpc = Assert.IsAssignableFrom<ControlPC>(okResult.Value);
            Assert.NotNull(controlpc);
            Assert.Equal(expectedControlPC.Id, controlpc.Id);
            Assert.Equal(expectedControlPC.userId, controlpc.userId);
            Assert.Equal(expectedControlPC.secret, controlpc.secret);
            Assert.Equal(expectedControlPC.meetputBroID, controlpc.meetputBroID);
            Assert.Equal(expectedControlPC.ControlPCSecret, controlpc.ControlPCSecret);
        }

        [Fact]
        public async Task CreateControlPC_Returns_InternalServerError_When_Exception_Is_Thrown()
        {
            // Arrange
            var controlPCController = CreateControlPCController();
            controlPCController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                    {
                        new Claim("Id", Guid.NewGuid().ToString())
                    }))
                }
            };

            _controlPCService.Setup(x => x.CreateAsync(It.IsAny<ControlPC>())).ThrowsAsync(new Exception());

            // Act
            var result = await controlPCController.CreateControlPC(new ControlPC(
                Guid.NewGuid(),
                "Secret",
                "BRO-1",
                "Secret controlpc"
            ));

            // Assert
            var objectres = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, objectres.StatusCode);
        }

        [Fact]
        public async Task UpdateControlPC_Returns_BadRequest_When_Invalid_ControlPC_Is_Passed()
        {
            // Arrange
            var controlPCController = CreateControlPCController();

            // Act
            var result = await controlPCController.UpdateControlPC(Guid.NewGuid(), null!);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UpdateControlPC_Returns_BadRequest_When_Invalid_ControlPC_ID_Is_Passed()
        {
            // Arrange
            var controlPCController = CreateControlPCController();

            // Act
            var result = await controlPCController.UpdateControlPC(Guid.NewGuid(), new ControlPC(
                Guid.NewGuid(),
                "Secret",
                "BRO-1",
                "Secret controlpc"
            ));

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UpdateControlPC_Returns_NotFound_When_ControlPC_Is_Not_Found()
        {
            // Arrange
            var controlPCController = CreateControlPCController();
            _controlPCService.Setup(x => x.GetAsync(It.IsAny<Guid>()))!.ReturnsAsync((ControlPC?)null);
            // Act
            var controlPC = new ControlPC(
                Guid.NewGuid(),
                "Secret",
                "BRO-1",
                "Secret controlpc"
            );
            var result = await controlPCController.UpdateControlPC(controlPC.Id, controlPC);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task UpdateControlPC_Returns_NoContent()
        {
            // Arrange
            var controlPCController = CreateControlPCController();
            var controlpc = new ControlPC(
                Guid.NewGuid(),
                "Secret",
                "BRO-1",
                "Secret controlpc"
            );

            _controlPCService.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(controlpc);
            var editedControlPC = controlpc;
            editedControlPC.secret = "EvenMoreSecret";
            // Act
            var result = await controlPCController.UpdateControlPC(editedControlPC.Id, editedControlPC);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateControlPC_Returns_InternalServerError_When_Exception_Is_Thrown()
        {
            // Arrange
            var controlPCController = CreateControlPCController();
            var controlpc = new ControlPC(
                Guid.NewGuid(),
                "Secret",
                "BRO-1",
                "Secret controlpc"
            );

            _controlPCService.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(controlpc);
            _controlPCRepository.Setup(x => x.UpdateAsync(It.IsAny<ControlPC>(), It.IsAny<Func<ControlPC, bool>>())).ThrowsAsync(new Exception());
            // Act
            var result = await controlPCController.UpdateControlPC(controlpc.Id, controlpc);

            // Assert
            var objectres = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectres.StatusCode);
        }

        [Fact]
        public async Task DeleteControlPC_Returns_NotFound_When_ControlPC_Is_Not_Found()
        {
            // Arrange
            var controlPCController = CreateControlPCController();
            _controlPCService.Setup(x => x.GetAsync(It.IsAny<Guid>()))!.ReturnsAsync((ControlPC?)null);
            // Act
            var result = await controlPCController.DeleteControlPC(Guid.NewGuid());

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteControlPC_Returns_NoContent()
        {
            // Arrange
            var controlPCController = CreateControlPCController();
            var controlpc = new ControlPC(
                Guid.NewGuid(),
                "Secret",
                "BRO-1",
                "Secret controlpc"
            );

            _controlPCService.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(controlpc);
            // Act
            var result = await controlPCController.DeleteControlPC(controlpc.Id);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

    }
}