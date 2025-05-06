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
        public async Task GetAllDoctors_WithValidController_ReturnsListOfDoctors()
        {
            // Arrange
            var _mock_repo = new Mock<IDoctorRepository>();
            var _fake_doctors = new List<Doctor>
            {
                new Doctor { UserId = 1, DepartmentId = 1, DoctorRating = 5.9, LicenseNumber = "numar" },
                new Doctor { UserId = 2, DepartmentId = 1, DoctorRating = 7.9, LicenseNumber = "alt_numar" },
            };
            _mock_repo.Setup(_repo => _repo.GetAllDoctorsAsync()).ReturnsAsync(_fake_doctors);

            var _controller = new DoctorController(_mock_repo.Object);

            // Act
            var _result = await _controller.GetAllDoctors();
            var _ok_result = _result.Result as OkObjectResult;

            // Assert
            Assert.IsNotNull(_ok_result);
            var _returned_doctors = _ok_result.Value as List<Doctor>;
            Assert.AreEqual(2, _returned_doctors.Count);
        }

        [TestMethod]
        public async Task GetDoctorByUserId_WithValidUserId_ReturnsDoctor()
        {
            // Arrange
            var _mock_repo = new Mock<IDoctorRepository>();
            var _fake_doctor = new Doctor { UserId = 1, DepartmentId = 1, DoctorRating = 5.9, LicenseNumber = "numar" };
            
            _mock_repo.Setup(_repo => _repo.GetDoctorByUserIdAsync(1)).ReturnsAsync(_fake_doctor);

            var _controller = new DoctorController(_mock_repo.Object);

            // Act
            var _result = await _controller.GetDoctorByUserId(1);
            var _ok_result = _result.Result as OkObjectResult;

            // Assert
            Assert.IsNotNull(_ok_result);
            var _returned_doctor = _ok_result.Value as Doctor;
            Assert.AreEqual(1, _returned_doctor.UserId);
        }

        [TestMethod]
        public async Task GetDoctorsByDepartmentId_ReturnsListOfDoctors()
        {
            // Arrange
            var _mock_repo = new Mock<IDoctorRepository>();
            var _fake_doctors = new List<Doctor>
            {
                new Doctor { UserId = 1, DepartmentId = 1, DoctorRating = 5.9, LicenseNumber = "numar" },
                new Doctor { UserId = 2, DepartmentId = 1, DoctorRating = 7.9, LicenseNumber = "alt_numar" },
                new Doctor { UserId = 3, DepartmentId = 2, DoctorRating = 7.9, LicenseNumber = "alt_alt_numar" },
            };
            _mock_repo.Setup(repo => repo.GetDoctorsByDepartmentIdAsync(1)).ReturnsAsync(_fake_doctors.Where(d => d.DepartmentId == 1).ToList());

            var _controller = new DoctorController(_mock_repo.Object);

            // Act
            var _result = await _controller.GetDoctorsByDepartmentId(1);
            var _ok_result = _result.Result as OkObjectResult;

            // Assert
            Assert.IsNotNull(_ok_result);
            var _returned_doctors = _ok_result.Value as List<Doctor>;
            Assert.AreEqual(2, _returned_doctors.Count);
        }

        [TestMethod]
        public async Task CreateDoctor_WithValidDoctor_ReturnsCreatedAtAction()
        {
            // Arrange
            var _mock_repo = new Mock<IDoctorRepository>();
            var _new_doctor = new Doctor { UserId = 5, DepartmentId = 2, DoctorRating = 9.0, LicenseNumber = "abc123" };

            _mock_repo.Setup(_repo => _repo.AddDoctorAsync(_new_doctor)).Returns(Task.CompletedTask);

            var _controller = new DoctorController(_mock_repo.Object);

            // Act
            var _result = await _controller.CreateDoctor(_new_doctor);

            // Assert
            var _created_at = _result as CreatedAtActionResult;
            Assert.IsNotNull(_created_at);
            Assert.AreEqual(nameof(_controller.GetDoctorByUserId), _created_at.ActionName);
            Assert.AreEqual(_new_doctor.UserId, ((Doctor)_created_at.Value).UserId);

            // Verify that AddDoctorAsync was called once
            _mock_repo.Verify(_repo => _repo.AddDoctorAsync(_new_doctor), Times.Once);
        }

        [TestMethod]
        public async Task DeleteDoctor_WithValidDoctorId_ReturnsNoContent()
        {
            // Arrange
            var _mock_repo = new Mock<IDoctorRepository>();
            int _doctor_id = 1;

            // No setup needed if the method succeeds (completes without exception)
            _mock_repo.Setup(_repo => _repo.DeleteDoctorAsync(_doctor_id)).Returns(Task.CompletedTask);

            var _controller = new DoctorController(_mock_repo.Object);

            // Act
            var _result = await _controller.DeleteDoctor(_doctor_id);

            // Assert
            Assert.IsInstanceOfType(_result, typeof(NoContentResult));
            _mock_repo.Verify(_repo => _repo.DeleteDoctorAsync(_doctor_id), Times.Once);
        }

    }
}
