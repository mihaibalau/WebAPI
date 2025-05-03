using ClassLibrary.Domain;
using ClassLibrary.IRepository;
using Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace WebApiTests;

[TestClass]
public class LogControllerTest
{
    [TestMethod]
    public async Task GetAllLogs_ReturnsListOfLogs()
    {
        // Arrange
        var mockRepo = new Mock<ILogRepository>();
        var fakeLogs = new List<Log>
        {
            new Log { LogId = 1, UserId = 1, ActionType = "Action 1", Timestamp = new DateTime(2025, 3, 2, 12, 32, 0) },
            new Log { LogId = 2, UserId = 1, ActionType = "Action 2", Timestamp = new DateTime(2025, 5, 1, 23, 59, 30) },
        };
        mockRepo.Setup(repo => repo.GetAllLogsAsync()).ReturnsAsync(fakeLogs);

        var controller = new LogController(mockRepo.Object);

        // Act
        var result = await controller.GetAllLogs();
        var okResult = result.Result as OkObjectResult;

        // Assert
        Assert.IsNotNull(okResult);
        var returnedLogs = okResult.Value as List<Log>;
        Assert.AreEqual(2, returnedLogs.Count);
    }

    [TestMethod]
    public async Task GetLogById_ReturnsLog()
    {
        // Arrange
        var mockRepo = new Mock<ILogRepository>();
        var fakeLog = new Log { LogId = 2, UserId = 1, ActionType = "Action 2", Timestamp = new DateTime(2025, 5, 1, 23, 59, 30) };

        mockRepo.Setup(repo => repo.GetLogByIdAsync(1)).ReturnsAsync(fakeLog);

        var controller = new LogController(mockRepo.Object);

        // Act
        var result = await controller.GetLogById(1);
        var okResult = result.Result as OkObjectResult;

        // Assert
        Assert.IsNotNull(okResult);
        var returnedLog = okResult.Value as Log;
        Assert.AreEqual(2, returnedLog.LogId);
    }

    [TestMethod]
    public async Task CreateLog_ReturnsCreatedAtAction()
    {
        // Arrange
        var mockRepo = new Mock<ILogRepository>();
        var fakeLog = new Log { LogId = 2, UserId = 1, ActionType = "Action 2", Timestamp = new DateTime(2025, 5, 1, 23, 59, 30) };
        mockRepo.Setup(repo => repo.AddLogAsync(fakeLog)).Returns(Task.CompletedTask);

        var controller = new LogController(mockRepo.Object);

        // Act
        var result = await controller.CreateLog(fakeLog);
        var createdAt = result as CreatedAtActionResult;

        // Assert
        Assert.IsNotNull(createdAt);
        Assert.AreEqual(fakeLog.LogId, ((Log)createdAt.Value).LogId);
    }

    [TestMethod]
    public async Task DeleteLog_ReturnsNoContent()
    {
        // Arrange
        var mockRepo = new Mock<ILogRepository>();
        int logId = 1;

        // No setup needed if the method succeeds (completes without exception)
        mockRepo.Setup(repo => repo.DeleteLogAsync(logId)).Returns(Task.CompletedTask);

        var controller = new LogController(mockRepo.Object);

        // Act
        var result = await controller.DeleteLog(logId);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NoContentResult));
        mockRepo.Verify(repo => repo.DeleteLogAsync(logId), Times.Once);
    }
}
