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
        var mock_repo = new Mock<IUserRepository>();
        var fake_users = new List<User>
        {
            new User { userId = 1, username = "john_sickko", password = "123456789", mail = "john@yahoo.com", role = "Patient", name = "John", birthDate = new DateOnly(1999, 03, 29), cnp = "1234567890123", address = "acasa locuiesc domle", phoneNumber = "07n-am cartela", registrationDate = new DateTime(2024, 03, 08, 19, 32, 0) },
            new User { userId = 2, username = "_xXx_paul_xXx_", password = "123456789", mail = "paul@yahoo.com", role = "Doctor", name = "John", birthDate = new DateOnly(2000, 02, 29), cnp = "1234567890123", address = "acasa locuiesc domle", phoneNumber = "07n-am cartela", registrationDate = new DateTime(2024, 03, 08, 19, 32, 0) }
        };
        mock_repo.Setup(repo => repo.getAllUsersAsync()).ReturnsAsync(fake_users);

        var controller = new UserController(mock_repo.Object);

        // Act
        var result = await controller.GetAllUsers();
        var ok_result = result.Result as OkObjectResult;

        // Assert
        Assert.IsNotNull(ok_result);
        var returned_users = ok_result.Value as List<User>;
        Assert.AreEqual(2, returned_users.Count);
    }

    [TestMethod]
    public async Task GetUserById_ReturnsUser()
    {
        // Arrange
        var mock_repo = new Mock<IUserRepository>();
        var fake_user = new User { userId = 1, username = "john__sickko", password = "123456789", mail = "john@yahoo.com", role = "Patient", name = "John", birthDate = new DateOnly(1999, 03, 29), cnp = "1234567890123", address = "acasa locuiesc domle", phoneNumber = "07n-am cartela", registrationDate = new DateTime(2024, 03, 08, 19, 32, 0) };
        mock_repo.Setup(repo => repo.getUserByIdAsync(1)).ReturnsAsync(fake_user);

        var controller = new UserController(mock_repo.Object);

        // Act
        var result = await controller.GetUserById(1);
        var ok_result = result.Result as OkObjectResult;

        // Assert
        Assert.IsNotNull(ok_result);
        var returned_user = ok_result.Value as User;
        Assert.AreEqual(1, returned_user.userId);
    }

    [TestMethod]
    public async Task CreateUser_ReturnsCreatedAtAction()
    {
        // Arrange
        var mock_repo = new Mock<IUserRepository>();
        var fake_user = new User { userId = 1, username = "john__sickko", password = "123456789", mail = "john@yahoo.com", role = "Patient", name = "John", birthDate = new DateOnly(1999, 03, 29), cnp = "1234567890123", address = "acasa locuiesc domle", phoneNumber = "07n-am cartela", registrationDate = new DateTime(2024, 03, 08, 19, 32, 0) };
        mock_repo.Setup(repo => repo.addUserAsync(fake_user)).Returns(Task.CompletedTask);

        var controller = new UserController(mock_repo.Object);

        // Act
        var result = await controller.CreateUser(fake_user);
        var created_at = result as CreatedAtActionResult;

        // Assert
        Assert.IsNotNull(created_at);
        Assert.AreEqual(fake_user.userId, ((User)created_at.Value).userId);
    }

    [TestMethod]
    public async Task DeleteUser_ReturnsNoContent()
    {
        // Arrange
        var mock_repo = new Mock<IUserRepository>();
        int user_id = 1;

        // No setup needed if the method succeeds (completes without exception)
        mock_repo.Setup(repo => repo.deleteUserAsync(user_id)).Returns(Task.CompletedTask);

        var controller = new UserController(mock_repo.Object);

        // Act
        var result = await controller.DeleteUser(user_id);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NoContentResult));
        mock_repo.Verify(repo => repo.deleteUserAsync(user_id), Times.Once);
    }
}
