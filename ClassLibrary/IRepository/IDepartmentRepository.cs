using System;
using System.Collections.Generic;
using Domain; 

namespace ClassLibrary.IRepository
{
    public interface IDepartmentRepository
    {
        /// <summary>
        /// Gets all departments.
        /// </summary>
        /// <returns>A list of departments.</returns>
        Task<List<Department>> GetAllDepartmentsAsync();

        /// <summary>
        /// Gets a department by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the department.</param>
        /// <returns>A department object if found, otherwise null.</returns>
        Task<Department> GetDepartmentByIdAsync(int id);

        /// <summary>
        /// Adds a new department to the system.
        /// </summary>
        /// <param name="department">The department to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddDepartmentAsync(Department department);

        /// <summary>
        /// Deletes a department by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the department to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteDepartmentAsync(int id);
    }
}
