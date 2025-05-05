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
            new User { UserId = 1, Username = "john_sickko", Password = "123456789", Mail = "john@yahoo.com", Role = "Patient", Name = "John", BirthDate = new DateOnly(1999, 03, 29), CNP = "1234567890123", Address = "acasa locuiesc domle", PhoneNumber = "07n-am cartela", RegistrationDate = new DateTime(2024, 03, 08, 19, 32, 0) },
            new User { UserId = 2, Username = "_xXx_paul_xXx_", Password = "123456789", Mail = "paul@yahoo.com", Role = "Doctor", Name = "John", BirthDate = new DateOnly(2000, 02, 29), CNP = "1234567890123", Address = "acasa locuiesc domle", PhoneNumber = "07n-am cartela", RegistrationDate = new DateTime(2024, 03, 08, 19, 32, 0) }
        };
        mock_repo.Setup(repo => repo.GetAllUsersAsync()).ReturnsAsync(fake_users);

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
        var fake_user = new User { UserId = 1, Username = "john__sickko", Password = "123456789", Mail = "john@yahoo.com", Role = "Patient", Name = "John", BirthDate = new DateOnly(1999, 03, 29), CNP = "1234567890123", Address = "acasa locuiesc domle", PhoneNumber = "07n-am cartela", RegistrationDate = new DateTime(2024, 03, 08, 19, 32, 0) };
        mock_repo.Setup(repo => repo.GetUserByIdAsync(1)).ReturnsAsync(fake_user);

        var controller = new UserController(mock_repo.Object);

        // Act
        var result = await controller.GetUserById(1);
        var ok_result = result.Result as OkObjectResult;

        // Assert
        Assert.IsNotNull(ok_result);
        var returned_user = ok_result.Value as User;
        Assert.AreEqual(1, returned_user.UserId);
    }

    [TestMethod]
    public async Task CreateUser_ReturnsCreatedAtAction()
    {
        // Arrange
        var mock_repo = new Mock<IUserRepository>();
        var fake_user = new User { UserId = 1, Username = "john__sickko", Password = "123456789", Mail = "john@yahoo.com", Role = "Patient", Name = "John", BirthDate = new DateOnly(1999, 03, 29), CNP = "1234567890123", Address = "acasa locuiesc domle", PhoneNumber = "07n-am cartela", RegistrationDate = new DateTime(2024, 03, 08, 19, 32, 0) };
        mock_repo.Setup(repo => repo.AddUserAsync(fake_user)).Returns(Task.CompletedTask);

        var controller = new UserController(mock_repo.Object);

        // Act
        var result = await controller.CreateUser(fake_user);
        var created_at = result as CreatedAtActionResult;

        // Assert
        Assert.IsNotNull(created_at);
        Assert.AreEqual(fake_user.UserId, ((User)created_at.Value).UserId);
    }

    [TestMethod]
    public async Task DeleteUser_ReturnsNoContent()
    {
        // Arrange
        var mock_repo = new Mock<IUserRepository>();
        int user_id = 1;

        // No setup needed if the method succeeds (completes without exception)
        mock_repo.Setup(repo => repo.DeleteUserAsync(user_id)).Returns(Task.CompletedTask);

        var controller = new UserController(mock_repo.Object);

        // Act
        var result = await controller.DeleteUser(user_id);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NoContentResult));
        mock_repo.Verify(repo => repo.DeleteUserAsync(user_id), Times.Once);
    }
}
