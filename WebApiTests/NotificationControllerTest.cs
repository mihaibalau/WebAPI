using ClassLibrary.Domain;
using ClassLibrary.IRepository;
using Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace WebApiTests;

[TestClass]
public class NotificationControllerTest
{
    [TestMethod]
    public async Task GetAllNotifications_ReturnsListOfNotification()
    {
        // Arrange
        var mockRepo = new Mock<INotificationRepository>();
        var fakeNotifications = new List<Notification>
        {
            new Notification { NotificationId = 1, UserId = 1, Message = "Message 1", DeliveryDateTime = new DateTime(2025, 3, 2, 12, 32, 0) },
            new Notification { NotificationId = 2, UserId = 1, Message = "Message 2", DeliveryDateTime = new DateTime(2025, 5, 1, 23, 59, 30) },
        };
        mockRepo.Setup(repo => repo.GetAllNotificationsAsync()).ReturnsAsync(fakeNotifications);

        var controller = new NotificationController(mockRepo.Object);

        // Act
        var result = await controller.GetAllNotifications();
        var okResult = result.Result as OkObjectResult;

        // Assert
        Assert.IsNotNull(okResult);
        var returnedNotifications = okResult.Value as List<Notification>;
        Assert.AreEqual(2, returnedNotifications.Count);
    }

    [TestMethod]
    public async Task GetNotificationByUserId_ReturnsListOfNotifications()
    {
        // Arrange
        var mockRepo = new Mock<INotificationRepository>();
        var fakeNotifications = new List<Notification>
        {
            new Notification { NotificationId = 1, UserId = 1, Message = "Message 1", DeliveryDateTime = new DateTime(2025, 3, 2, 12, 32, 0) },
            new Notification { NotificationId = 2, UserId = 1, Message = "Message 2", DeliveryDateTime = new DateTime(2025, 5, 1, 23, 59, 30) },
        }; 
        mockRepo.Setup(repo => repo.GetNotificationsByUserIdAsync(1)).ReturnsAsync(fakeNotifications.Where(d => d.UserId == 1).ToList());

        var controller = new NotificationController(mockRepo.Object);

        // Act
        var result = await controller.GetNotificationsByUserId(1);
        var okResult = result.Result as OkObjectResult;

        // Assert
        Assert.IsNotNull(okResult);
        var returnedNotifications = okResult.Value as List<Notification>;
        Assert.AreEqual(2, returnedNotifications.Count);
    }

    [TestMethod]
    public async Task CreateNotification_ReturnsCreatedAtAction()
    {
        // Arrange
        var mockRepo = new Mock<INotificationRepository>();
        var fakeNotification = new Notification { NotificationId = 1, UserId = 1, Message = "Message 1", DeliveryDateTime = new DateTime(2025, 3, 2, 12, 32, 0) };
        mockRepo.Setup(repo => repo.AddNotificationAsync(fakeNotification)).Returns(Task.CompletedTask);

        var controller = new NotificationController(mockRepo.Object);

        // Act
        var result = await controller.CreateNotification(fakeNotification);
        var createdAt = result as CreatedAtActionResult;

        // Assert
        Assert.IsNotNull(createdAt);
        Assert.AreEqual(fakeNotification.NotificationId, ((Notification)createdAt.Value).NotificationId);
    }

    [TestMethod]
    public async Task DeleteNotification_ReturnsNoContent()
    {
        // Arrange
        var mockRepo = new Mock<INotificationRepository>();
        int notificationId = 1;

        // No setup needed if the method succeeds (completes without exception)
        mockRepo.Setup(repo => repo.DeleteNotificationAsync(notificationId)).Returns(Task.CompletedTask);

        var controller = new NotificationController(mockRepo.Object);

        // Act
        var result = await controller.DeleteNotification(notificationId);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NoContentResult));
        mockRepo.Verify(repo => repo.DeleteNotificationAsync(notificationId), Times.Once);
    }
}
