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
        public async Task GetAllDepartments_WiithValidDoctorDepartments_ReturnsListOfDepartments()
        {
            // Arrange
            var _mock_repo = new Mock<IDepartmentRepository>();
            var _fake_departments = new List<Department>
            {
                new Department { Id = 1, Name = "John" },
                new Department { Id = 2, Name = "Johnny" },
            };
            _mock_repo.Setup(_repo => _repo.GetAllDepartmentsAsync()).ReturnsAsync(_fake_departments);

            var _controller = new DepartmentController(_mock_repo.Object);

            // Act
            var _result = await _controller.GetAllDepartments();
            var _ok_result = _result.Result as OkObjectResult;

            // Assert
            Assert.IsNotNull(_ok_result);
            var returnedDepartments = _ok_result.Value as List<Department>;
            Assert.AreEqual(2, returnedDepartments.Count);
        }

        [TestMethod]
        public async Task CreateDepartment_WithValidDepartmentInput_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var _mock_repo = new Mock<IDepartmentRepository>();
            var _fake_department = new Department { Id = 1, Name = "John" };
            _mock_repo.Setup(_repo => _repo.AddDepartmentAsync(_fake_department)).Returns(Task.CompletedTask);

            var _controller = new DepartmentController(_mock_repo.Object);

            // Act
            var _result = await _controller.CreateDepartment(_fake_department);
            var _created_at = _result as CreatedAtActionResult;

            // Assert
            Assert.IsNotNull(_created_at);
            Assert.AreEqual(_fake_department.Id, ((Department)_created_at.Value).Id);
        }

        [TestMethod]
        public async Task DeleteDepartment_WithValidDoctorID_ReturnsNoContent()
        {
            // Arrange
            var _mock_repo = new Mock<IDepartmentRepository>();
            int _doctor_id = 1;

            // No setup needed if the method succeeds (completes without exception)
            _mock_repo.Setup(_repo => _repo.DeleteDepartmentAsync(_doctor_id)).Returns(Task.CompletedTask);

            var _controller = new DepartmentController(_mock_repo.Object);

            // Act
            var _result = await _controller.DeleteDepartment(_doctor_id);

            // Assert
            Assert.IsInstanceOfType(_result, typeof(NoContentResult));
            _mock_repo.Verify(repo => repo.DeleteDepartmentAsync(_doctor_id), Times.Once);
        }
    }
}
