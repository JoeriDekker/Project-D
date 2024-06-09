using Microsoft.AspNetCore.Mvc;
using Moq;
using WAMServer.Controllers;
using WAMServer.Interfaces;
using WAMServer.Models;

namespace WAMServer.Tests.Controllers
{
    public class WaterStorageControllerTests
    {
        private Mock<IRepository<WaterStorage>> _waterStorageRepository;
        private Mock<IRepository<User>> _userRepository;
        private Mock<IRepository<ControlPC>> _controlPCRepository;

        public WaterStorageControllerTests()
        {
            _waterStorageRepository = new Mock<IRepository<WaterStorage>>();
            _userRepository = new Mock<IRepository<User>>();
            _controlPCRepository = new Mock<IRepository<ControlPC>>();
        }

        public WaterStorageController CreateWaterStorageController()
        {
            return new WaterStorageController(_waterStorageRepository.Object, _userRepository.Object, _controlPCRepository.Object);
        }

        [Fact]
        public void GetWaterStorageFromID_When_ControlPC_Is_Null_Returns_NotFound()
        {
            // Arrange
            var controller = CreateWaterStorageController();
            var id = Guid.NewGuid();
            _controlPCRepository.Setup(x => x.GetAll(It.IsAny<Func<ControlPC, bool>>())).Returns(new List<ControlPC>());

            // Act
            var result = controller.GetWaterStorageFromID(id);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetWaterStorageFromID_When_Waterstorage_Is_Null_Returns_NotFound()
        {
            // Arrange
            var controller = CreateWaterStorageController();
            var id = Guid.NewGuid();
            _controlPCRepository.Setup(x => x.GetAll(It.IsAny<Func<ControlPC, bool>>())).Returns(new List<ControlPC> { new ControlPC(id, "secret", "Bro-1", "Moresecret")  });
            _waterStorageRepository.Setup(x => x.GetAll(It.IsAny<Func<WaterStorage, bool>>())).Returns((List<WaterStorage>?)null!);

            // Act
            var result = controller.GetWaterStorageFromID(id);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);

        }

        [Fact]
        public void GetWaterStorageFromID_When_Waterstorage_Is_Found_Returns_Ok()
        {
            // Arrange
            var controller = CreateWaterStorageController();
            var id = Guid.NewGuid();
            var controlPC = new ControlPC(id, "secret", "bro-1", "secretsecret");
            _controlPCRepository.Setup(x => x.GetAll(It.IsAny<Func<ControlPC, bool>>())).Returns(new List<ControlPC> { controlPC });
            _waterStorageRepository.Setup(x => x.GetAll(It.IsAny<Func<WaterStorage, bool>>())).Returns(new List<WaterStorage> { new WaterStorage(controlPC.Id, "Sewege Water", 0, "Gouda", 0) });

            // Act
            var result = controller.GetWaterStorageFromID(id);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

    }
}