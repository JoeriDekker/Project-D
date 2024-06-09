using Microsoft.AspNetCore.Mvc;
using Moq;
using WAMServer.Controllers;
using WAMServer.Interfaces;
using WAMServer.Models;

namespace WAMServer.Tests.Controllers
{
    public class GroundWaterLogControllerTests
    {
        private readonly Mock<IRepository<GroundWaterLog>> _mockRepository;

        public GroundWaterLogControllerTests()
        {
            _mockRepository = new Mock<IRepository<GroundWaterLog>>();
        }

        [Fact]
        public async Task GetGroundWaterLogs_ReturnsGroundWaterLogs()
        {
            // Arrange
            var controller = new GroundWaterLogController(_mockRepository.Object);
            var expectedGroundWaterLogs = new List<GroundWaterLog>
            {
                new GroundWaterLog("ControlPC1", DateTime.UtcNow, -3.0m),
                new GroundWaterLog("ControlPC2", DateTime.UtcNow, -2.0m),
                new GroundWaterLog("ControlPC3", DateTime.UtcNow, -1.0m)
            };

            _mockRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(expectedGroundWaterLogs);

            // Act
            var result = await controller.GetGroundWaterLogs();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualGroundWaterLogs = Assert.IsAssignableFrom<IEnumerable<GroundWaterLog>>(okResult.Value);
            Assert.Equal(expectedGroundWaterLogs, actualGroundWaterLogs);
        }

        [Fact]
        public async Task GetGroundWaterLogs_ReturnsNoGroundWaterLogs()
        {
            // Arrange
            var controller = new GroundWaterLogController(_mockRepository.Object);
            var expectedGroundWaterLogs = new List<GroundWaterLog>();

            _mockRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(expectedGroundWaterLogs);

            // Act
            var result = await controller.GetGroundWaterLogs();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualGroundWaterLogs = Assert.IsAssignableFrom<IEnumerable<GroundWaterLog>>(okResult.Value);
            Assert.Empty(actualGroundWaterLogs);
        }
    }
}