using Microsoft.AspNetCore.Mvc;
using MimeKit.Cryptography;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAMServer.Controllers;
using WAMServer.Interfaces;
using WAMServer.Models;

namespace server.tests
{
    public class ActionLogControllerTests
    {
        private readonly Mock<IRepository<ActionLog>> _actionLogRepository;
        public ActionLogControllerTests()
        {
            _actionLogRepository = new Mock<IRepository<ActionLog>>();
        }

        [Fact]
        public async Task GetActionLogs_Returns_Empty_List_When_No_Logs()
        {
            // Arrange
            var mockActionLogs = new List<ActionLog>();  // Ensure this is non-null
            _actionLogRepository.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(mockActionLogs);

            var controller = new ActionLogController(_actionLogRepository.Object);

            // Act
            var result = await controller.GetActionLogs();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(mockActionLogs, okResult.Value as List<ActionLog>);
        }

        public static IEnumerable<object[]> GetActionLogs_Returns_Desired_Lists_Data()
        {
            return new List<object[]>
            {
                new object[] { new List<ActionLog>() { new ActionLog(Guid.NewGuid(), 1, DateTime.UtcNow) } }, // Single log
                new object[] { new List<ActionLog>() {
                    new ActionLog(Guid.NewGuid(), 1, DateTime.UtcNow),
                    new ActionLog(Guid.NewGuid(), 2, DateTime.UtcNow),
                    new ActionLog(Guid.NewGuid(), 3, DateTime.UtcNow),
                    new ActionLog(Guid.NewGuid(), 4, DateTime.UtcNow)
                }
                }
            };
        }

        [Theory]
        [MemberData(nameof(GetActionLogs_Returns_Desired_Lists_Data))]
        public async Task GetActionLogs_Returns_Desired_Lists(List<ActionLog> actionLogs)
        {
            // Arrange
            _actionLogRepository.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(actionLogs);

            var controller = new ActionLogController(_actionLogRepository.Object);

            // Act
            var result = await controller.GetActionLogs();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(actionLogs, okResult.Value as List<ActionLog>);
        }

    }
}
