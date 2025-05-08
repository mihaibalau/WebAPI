using Microsoft.AspNetCore.Mvc;
using Data;
using Entity;
using ClassLibrary.IRepository;
using ClassLibrary.Domain;

namespace Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository _userRepository)
        {
            this.userRepository = _userRepository;
        }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>An ActionResult containing a list of users.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            try
            {
                List<User> users = await this.userRepository.GetAllUsersAsync();
                return this.Ok(users);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while retrieving users. Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>An ActionResult containing the user.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            try
            {
                User user = await this.userRepository.GetUserByIdAsync(id);
                if (user == null)
                {
                    return this.NotFound($"User with ID {id} was not found.");
                }
                return this.Ok(user);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while retrieving the user. Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="user">The user entity to create.</param>
        /// <returns>An ActionResult indicating success or failure.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateUser([FromBody] User user)
        {
            if (user == null)
            {
                return this.BadRequest("Valid user entity is required.");
            }

            try
            {
                await this.userRepository.AddUserAsync(user);
                return this.CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while creating user. Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="user">The updated user data.</param>
        /// <returns>An ActionResult indicating success or failure.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateUser(int id, [FromBody] User user)
        {
            if (user == null)
            {
                return this.BadRequest("Valid user entity is required.");
            }

            if (id != user.UserId)
            {
                return this.BadRequest("User ID in the URL must match the ID in the request body.");
            }

            try
            {
                await this.userRepository.UpdateUserAsync(user);
                return this.NoContent();
            }
            catch (KeyNotFoundException)
            {
                return this.NotFound($"User with ID {id} was not found.");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while updating user. Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>An ActionResult indicating success or failure.</returns>
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                await this.userRepository.DeleteUserAsync(id);
                return this.NoContent();
            }
            catch (KeyNotFoundException)
            {
                return this.NotFound($"User with ID {id} was not found.");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while deleting user. Error: {ex.Message}");
            }
        }


        /// <summary>
        /// Retrieves all users by role.
        /// </summary>
        /// <param name="role">The role to filter users by.</param>
        /// <returns>An ActionResult containing a list of users.</returns>
        [HttpGet("role/{role}")]
        [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<User>>> GetUsersByRole(string role)
        {
            try
            {
                List<User> users = await this.userRepository.GetUsersByRoleAsync(role);

                if (users == null || users.Count == 0)
                {
                    return this.NotFound($"No users found with role '{role}'.");
                }

                return this.Ok(users);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while retrieving users by role. Error: {ex.Message}");
            }
        }
        /// <summary>
        /// Retrieves all users that match a given name.
        /// </summary>
        /// <param name="name">The name to search for.</param>
        /// <returns>List of users with the specified name.</returns>
        [HttpGet("by-name/{name}")]
        [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<User>>> GetUsersByName(string name)
        {
            try
            {
                var users = await this.userRepository.GetUsersByNameAsync(name);
                return this.Ok(users);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving users by name. Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a user by their CNP.
        /// </summary>
        /// <param name="cnp">The CNP of the user to search for.</param>
        /// <returns>A user with the specified CNP.</returns>
        [HttpGet("by-cnp/{cnp}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<User>> GetUserByCNP(string cnp)
        {
            try
            {
                var user = await this.userRepository.GetUserByCNPAsync(cnp);
                if (user == null)
                {
                    return this.NotFound($"User with CNP {cnp} not found.");
                }
                return this.Ok(user);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving user by CNP. Error: {ex.Message}");
            }
        }
    }
}
