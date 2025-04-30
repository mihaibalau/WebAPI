using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;

namespace Controllers
{
    [Route("api/departments")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DepartmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/departments
        /// <summary>
        /// Retrieves all departments.
        /// </summary>
        /// <returns>An ActionResult containing a list of departments.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Department>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Department>>> GetDepartments()
        {
            try
            {
                var departments = await _context.Departments.ToListAsync();
                return Ok(departments);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while retrieving departments. Error: {ex.Message}");
            }
        }

        // POST: api/departments
        /// <summary>
        /// Creates a new department.
        /// </summary>
        /// <param name="department">The department to create.</param>
        /// <returns>An ActionResult indicating the result of the operation.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Department), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Department>> CreateDepartment([FromBody] Department department)
        {
            try
            {
                _context.Departments.Add(department);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetDepartmentById), new { id = department.Id }, department);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while creating the department. Error: {ex.Message}");
            }
        }

        // DELETE: api/departments/{id}
        /// <summary>
        /// Deletes a department by ID.
        /// </summary>
        /// <param name="id">The ID of the department to delete.</param>
        /// <returns>An ActionResult indicating the result of the operation.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteDepartment(int id)
        {
            try
            {
                var department = await _context.Departments.FindAsync(id);
                if (department == null)
                {
                    return NotFound($"Department with ID {id} was not found.");
                }

                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while deleting the department. Error: {ex.Message}");
            }
        }

        // GET: api/departments/{id}
        /// <summary>
        /// Retrieves a department by ID.
        /// </summary>
        /// <param name="id">The ID of the department.</param>
        /// <returns>An ActionResult containing the department.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Department), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Department>> GetDepartmentById(int id)
        {
            try
            {
                var department = await _context.Departments.FindAsync(id);
                if (department == null)
                {
                    return NotFound($"Department with ID {id} was not found.");
                }

                return Ok(department);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while retrieving the department. Error: {ex.Message}");
            }
        }

        // GET: api/departments/by-name?name=Finance
        /// <summary>
        /// Retrieves a department by its name.
        /// </summary>
        /// <param name="name">The name of the department.</param>
        /// <returns>An ActionResult containing the department.</returns>
        [HttpGet("by-name")]
        [ProducesResponseType(typeof(Department), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Department>> GetDepartmentByName([FromQuery] string name)
        {
            try
            {
                var department = await _context.Departments
                    .FirstOrDefaultAsync(d => d.Name.ToLower() == name.ToLower());

                if (department == null)
                {
                    return NotFound($"Department with name '{name}' was not found.");
                }

                return Ok(department);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while retrieving the department. Error: {ex.Message}");
            }
        }
    }
}
