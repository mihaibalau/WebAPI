using ClassLibrary.Domain;
using ClassLibrary.IRepository;
using Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApiTests
{
    [TestClass]
    public class DepartmentControllerTest
    {
        [TestMethod]
        public async Task GetAllDoctors_ReturnsListOfDepartments()
        {
            // Arrange
            var mockRepo = new Mock<IDepartmentRepository>();
            var fakeDepartments = new List<Department>
            {
                new Department { Id = 1, Name = "John" },
                new Department { Id = 2, Name = "Johnny" },
            };
            mockRepo.Setup(repo => repo.GetAllDepartmentsAsync()).ReturnsAsync(fakeDepartments);

            var controller = new DepartmentController(mockRepo.Object);

            // Act
            var result = await controller.GetAllDepartments();
            var okResult = result.Result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            var returnedDepartments = okResult.Value as List<Department>;
            Assert.AreEqual(2, returnedDepartments.Count);
        }

        [TestMethod]
        public async Task CreateDepartment_ReturnsCreatedAtAction()
        {
            // Arrange
            var mockRepo = new Mock<IDepartmentRepository>();
            var fakeDepartment = new Department { Id = 1, Name = "John" };
            mockRepo.Setup(repo => repo.AddDepartmentAsync(fakeDepartment)).Returns(Task.CompletedTask);

            var controller = new DepartmentController(mockRepo.Object);

            // Act
            var result = await controller.CreateDepartment(fakeDepartment);
            var createdAt = result as CreatedAtActionResult;

            // Assert
            Assert.IsNotNull(createdAt);
            Assert.AreEqual(fakeDepartment.Id, ((Department)createdAt.Value).Id);
        }

        [TestMethod]
        public async Task DeleteDepartment_ReturnsNoContent()
        {
            // Arrange
            var mockRepo = new Mock<IDepartmentRepository>();
            int doctorId = 1;

            // No setup needed if the method succeeds (completes without exception)
            mockRepo.Setup(repo => repo.DeleteDepartmentAsync(doctorId)).Returns(Task.CompletedTask);

            var controller = new DepartmentController(mockRepo.Object);

            // Act
            var result = await controller.DeleteDepartment(doctorId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            mockRepo.Verify(repo => repo.DeleteDepartmentAsync(doctorId), Times.Once);
        }
    }
}
