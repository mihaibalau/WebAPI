using ClassLibrary.Domain;
using ClassLibrary.IRepository;
using Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace WebApiTests;

[TestClass]
public class UserControllerTest
{
    [TestMethod]
    public async Task GetAllUsers_ReturnsListOfUsers()
    {
        // Arrange
        var mockRepo = new Mock<IUserRepository>();
        var fakeUsers = new List<User>
        {
            new User { UserId = 1, Username = "john_sickko", Password = "123456789", Mail = "john@yahoo.com", Role = "Patient", Name = "John", BirthDate = new DateOnly(1999, 03, 29), CNP = "1234567890123", Address = "acasa locuiesc domle", PhoneNumber = "07n-am cartela", RegistrationDate = new DateTime(2024, 03, 08, 19, 32, 0) },
            new User { UserId = 2, Username = "_xXx_paul_xXx_", Password = "123456789", Mail = "paul@yahoo.com", Role = "Doctor", Name = "John", BirthDate = new DateOnly(2000, 02, 29), CNP = "1234567890123", Address = "acasa locuiesc domle", PhoneNumber = "07n-am cartela", RegistrationDate = new DateTime(2024, 03, 08, 19, 32, 0) }
        };
        mockRepo.Setup(repo => repo.GetAllUsersAsync()).ReturnsAsync(fakeUsers);

        var controller = new UserController(mockRepo.Object);

        // Act
        var result = await controller.GetAllUsers();
        var okResult = result.Result as OkObjectResult;

        // Assert
        Assert.IsNotNull(okResult);
        var returnedUsers = okResult.Value as List<User>;
        Assert.AreEqual(2, returnedUsers.Count);
    }

    [TestMethod]
    public async Task GetUserById_ReturnsUser()
    {
        // Arrange
        var mockRepo = new Mock<IUserRepository>();
        var fakeUser = new User { UserId = 1, Username = "john__sickko", Password = "123456789", Mail = "john@yahoo.com", Role = "Patient", Name = "John", BirthDate = new DateOnly(1999, 03, 29), CNP = "1234567890123", Address = "acasa locuiesc domle", PhoneNumber = "07n-am cartela", RegistrationDate = new DateTime(2024, 03, 08, 19, 32, 0) };
        mockRepo.Setup(repo => repo.GetUserByIdAsync(1)).ReturnsAsync(fakeUser);

        var controller = new UserController(mockRepo.Object);

        // Act
        var result = await controller.GetUserById(1);
        var okResult = result.Result as OkObjectResult;

        // Assert
        Assert.IsNotNull(okResult);
        var returnedUser = okResult.Value as User;
        Assert.AreEqual(1, returnedUser.UserId);
    }

    [TestMethod]
    public async Task CreateUser_ReturnsCreatedAtAction()
    {
        // Arrange
        var mockRepo = new Mock<IUserRepository>();
        var fakeUser = new User { UserId = 1, Username = "john__sickko", Password = "123456789", Mail = "john@yahoo.com", Role = "Patient", Name = "John", BirthDate = new DateOnly(1999, 03, 29), CNP = "1234567890123", Address = "acasa locuiesc domle", PhoneNumber = "07n-am cartela", RegistrationDate = new DateTime(2024, 03, 08, 19, 32, 0) };
        mockRepo.Setup(repo => repo.AddUserAsync(fakeUser)).Returns(Task.CompletedTask);

        var controller = new UserController(mockRepo.Object);

        // Act
        var result = await controller.CreateUser(fakeUser);
        var createdAt = result as CreatedAtActionResult;

        // Assert
        Assert.IsNotNull(createdAt);
        Assert.AreEqual(fakeUser.UserId, ((User)createdAt.Value).UserId);
    }

    [TestMethod]
    public async Task DeleteUser_ReturnsNoContent()
    {
        // Arrange
        var mockRepo = new Mock<IUserRepository>();
        int UserId = 1;

        // No setup needed if the method succeeds (completes without exception)
        mockRepo.Setup(repo => repo.DeleteUserAsync(UserId)).Returns(Task.CompletedTask);

        var controller = new UserController(mockRepo.Object);

        // Act
        var result = await controller.DeleteUser(UserId);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NoContentResult));
        mockRepo.Verify(repo => repo.DeleteUserAsync(UserId), Times.Once);
    }
}
