using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Controllers;
using ClassLibrary.IRepository;
using ClassLibrary.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApiTests
{
    [TestClass]
    public class DoctorControllerTests
    {
        [TestMethod]
        public async Task GetAllDoctors_ReturnsListOfDoctors()
        {
            // Arrange
            var mockRepo = new Mock<IDoctorRepository>();
            var fakeDoctors = new List<Doctor>
            {
                new Doctor { UserId = 1, DepartmentId = 1, DoctorRating = 5.9, LicenseNumber = "numar" },
                new Doctor { UserId = 2, DepartmentId = 1, DoctorRating = 7.9, LicenseNumber = "alt_numar" },
            };
            mockRepo.Setup(repo => repo.GetAllDoctorsAsync()).ReturnsAsync(fakeDoctors);

            var controller = new DoctorController(mockRepo.Object);

            // Act
            var result = await controller.GetAllDoctors();
            var okResult = result.Result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            var returnedDoctors = okResult.Value as List<Doctor>;
            Assert.AreEqual(2, returnedDoctors.Count);
        }

        [TestMethod]
        public async Task GetDoctorByUserId_ReturnsDoctor()
        {
            // Arrange
            var mockRepo = new Mock<IDoctorRepository>();
            var fakeDoctor = new Doctor { UserId = 1, DepartmentId = 1, DoctorRating = 5.9, LicenseNumber = "numar" };
            
            mockRepo.Setup(repo => repo.GetDoctorByUserIdAsync(1)).ReturnsAsync(fakeDoctor);

            var controller = new DoctorController(mockRepo.Object);

            // Act
            var result = await controller.GetDoctorByUserId(1);
            var okResult = result.Result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            var returnedDoctor = okResult.Value as Doctor;
            Assert.AreEqual(1, returnedDoctor.UserId);
        }

        [TestMethod]
        public async Task GetDoctorsByDepartmentId_ReturnsListOfDoctors()
        {
            // Arrange
            var mockRepo = new Mock<IDoctorRepository>();
            var fakeDoctors = new List<Doctor>
            {
                new Doctor { UserId = 1, DepartmentId = 1, DoctorRating = 5.9, LicenseNumber = "numar" },
                new Doctor { UserId = 2, DepartmentId = 1, DoctorRating = 7.9, LicenseNumber = "alt_numar" },
                new Doctor { UserId = 3, DepartmentId = 2, DoctorRating = 7.9, LicenseNumber = "alt_alt_numar" },
            };
            mockRepo.Setup(repo => repo.GetDoctorsByDepartmentIdAsync(1)).ReturnsAsync(fakeDoctors.Where(d => d.DepartmentId == 1).ToList());

            var controller = new DoctorController(mockRepo.Object);

            // Act
            var result = await controller.GetDoctorsByDepartmentId(1);
            var okResult = result.Result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            var returnedDoctors = okResult.Value as List<Doctor>;
            Assert.AreEqual(2, returnedDoctors.Count);
        }

        [TestMethod]
        public async Task CreateDoctor_ReturnsCreatedAtAction()
        {
            // Arrange
            var mockRepo = new Mock<IDoctorRepository>();
            var newDoctor = new Doctor { UserId = 5, DepartmentId = 2, DoctorRating = 9.0, LicenseNumber = "abc123" };

            mockRepo.Setup(repo => repo.AddDoctorAsync(newDoctor)).Returns(Task.CompletedTask);

            var controller = new DoctorController(mockRepo.Object);

            // Act
            var result = await controller.CreateDoctor(newDoctor);

            // Assert
            var createdAt = result as CreatedAtActionResult;
            Assert.IsNotNull(createdAt);
            Assert.AreEqual(nameof(controller.GetDoctorByUserId), createdAt.ActionName);
            Assert.AreEqual(newDoctor.UserId, ((Doctor)createdAt.Value).UserId);

            // Verify that AddDoctorAsync was called once
            mockRepo.Verify(repo => repo.AddDoctorAsync(newDoctor), Times.Once);
        }

        [TestMethod]
        public async Task DeleteDoctor_ReturnsNoContent()
        {
            // Arrange
            var mockRepo = new Mock<IDoctorRepository>();
            int doctorId = 1;

            // No setup needed if the method succeeds (completes without exception)
            mockRepo.Setup(repo => repo.DeleteDoctorAsync(doctorId)).Returns(Task.CompletedTask);

            var controller = new DoctorController(mockRepo.Object);

            // Act
            var result = await controller.DeleteDoctor(doctorId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            mockRepo.Verify(repo => repo.DeleteDoctorAsync(doctorId), Times.Once);
        }

    }
}
