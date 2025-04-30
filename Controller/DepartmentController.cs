using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;
using ClassLibrary.IRepository;
using Domain;

namespace Controllers
{
    [Route("api/department")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository departmentRepository;

        public DepartmentController(IDepartmentRepository _departmentRepository)
        {
            this.departmentRepository = _departmentRepository;
        }

        /// <summary>
        /// Retrieves all departments.
        /// </summary>
        /// <returns>An ActionResult containing a list of departments.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Department>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Department>>> GetAllDepartments()
        {
            try
            {
                List<Department> departments = await this.departmentRepository.GetAllDepartmentsAsync();
                return this.Ok(departments);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while retrieving departments. Error: {ex.Message}");
            }
        }

    }
}
