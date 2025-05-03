using ClassLibrary.Domain;
using ClassLibrary.IRepository;
using Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace WebApiTests;

[TestClass]
public class PatientControllerTest
{
    [TestMethod]
    public async Task GetAllPatients_ReturnsListOfPatients()
    {
        // Arrange
        var mockRepo = new Mock<IPatientRepository>();
        var fakePatients = new List<Patient>
        {
            new Patient { UserId = 1, BloodType = "AB+", EmergencyContact = "Mom", Allergies = "cats, dogs", Weight = 72.6, Height = 167 },
            new Patient { UserId = 2, BloodType = "A-", EmergencyContact = "Dad", Allergies = "cats, dogs", Weight = 92.6, Height = 199 }
        };
        mockRepo.Setup(repo => repo.GetAllPatientsAsync()).ReturnsAsync(fakePatients);

        var controller = new PatientController(mockRepo.Object);

        // Act
        var result = await controller.GetAllPatients();
        var okResult = result.Result as OkObjectResult;

        // Assert
        Assert.IsNotNull(okResult);
        var returnedPatients = okResult.Value as List<Patient>;
        Assert.AreEqual(2, returnedPatients.Count);
    }

    [TestMethod]
    public async Task GetPatientById_ReturnsPatient()
    {
        // Arrange
        var mockRepo = new Mock<IPatientRepository>();
        var fakePatient = new Patient { UserId = 2, BloodType = "A-", EmergencyContact = "Dad", Allergies = "cats, dogs", Weight = 92.6, Height = 199 };

        mockRepo.Setup(repo => repo.GetPatientByUserIdAsync(2)).ReturnsAsync(fakePatient);

        var controller = new PatientController(mockRepo.Object);

        // Act
        var result = await controller.GetPatientById(2);
        var okResult = result.Result as OkObjectResult;

        // Assert
        Assert.IsNotNull(okResult);
        var returnedPatient = okResult.Value as Patient;
        Assert.AreEqual(2, returnedPatient.UserId);
    }

    [TestMethod]
    public async Task CreatePatient_ReturnsCreatedAtAction()
    {
        // Arrange
        var mockRepo = new Mock<IPatientRepository>();
        var fakePatient = new Patient { UserId = 2, BloodType = "A-", EmergencyContact = "Dad", Allergies = "cats, dogs", Weight = 92.6, Height = 199 };
        mockRepo.Setup(repo => repo.AddPatientAsync(fakePatient)).Returns(Task.CompletedTask);

        var controller = new PatientController(mockRepo.Object);

        // Act
        var result = await controller.CreatePatient(fakePatient);
        var createdAt = result as CreatedAtActionResult;

        // Assert
        Assert.IsNotNull(createdAt);
        Assert.AreEqual(fakePatient.UserId, ((Patient)createdAt.Value).UserId);
    }

    [TestMethod]
    public async Task DeletePatient_ReturnsNoContent()
    {
        // Arrange
        var mockRepo = new Mock<IPatientRepository>();
        int PatientId = 1;

        // No setup needed if the method succeeds (completes without exception)
        mockRepo.Setup(repo => repo.DeletePatientAsync(PatientId)).Returns(Task.CompletedTask);

        var controller = new PatientController(mockRepo.Object);

        // Act
        var result = await controller.DeletePatient(PatientId);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NoContentResult));
        mockRepo.Verify(repo => repo.DeletePatientAsync(PatientId), Times.Once);
    }
}
