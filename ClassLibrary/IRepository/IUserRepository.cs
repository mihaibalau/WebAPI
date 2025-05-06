using ClassLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.IRepository
{
    public interface IUserRepository
    {
        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns> A list of users.</returns>
        Task<List<User>> GetAllUsersAsync();

        /// <summary>
        /// Gets a user by its unique identifier.
        /// </summary>
        /// <param name="id"> the id of the user.</param>
        /// <returns> The user with the given id.</returns>
        Task<User> GetUserByIdAsync(int id);

        /// <summary>
        /// Gets a user by its username.
        /// </summary>
        /// <param name="user"> The user to be searched.</param>
        /// <returns> The user with the given username.</returns>
        Task AddUserAsync(User user);

        /// <summary>
        /// Deletes a user by its unique identifier.
        /// </summary>
        /// <param name="id"> The id of the user</param>
        /// <returns> task representing the asynchronous operation.</returns>
        Task DeleteUserAsync(int id);

        /// <summary>
        /// Retrieves all users that match the specified role.
        /// </summary>
        /// <param name="role">The role used to filter users (e.g., "Admin", "User").</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the list of users with the specified role.</returns>
        Task<List<User>> GetUsersByRoleAsync(string role);
       
        /// <summary>
        /// Retrieves all users that match a given name.
        /// </summary>
        /// <param name="name">The name to search for.</param>
        /// <returns>A list of users with the specified name.</returns>
        Task<List<User>> GetUsersByNameAsync(string name);

    }
}
