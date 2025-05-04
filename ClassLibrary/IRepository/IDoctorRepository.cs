using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.IRepository
{
   public interface IDoctorRepository
    {
        /// <summary>
        /// Gets all doctors.
        /// </summary>
        /// <returns>A list of doctors</returns>
        Task<List<Doctor>> GetAllDoctorsAsync();

        /// <summary>
        /// Gets a doctor by its unique identifier.
        /// </summary>
        /// <param name="id">The id of the user</param>
        /// <returns>The doctor with the given user id.</returns>
        Task<Doctor> GetDoctorByUserIdAsync(int id);

        /// <summary>
        /// Gets a list of doctors by department id.
        /// </summary>
        /// <param name="departmentId">The id of the departemnt</param>
        /// <returns>The list of doctors with the given department id</returns>
        Task<List<Doctor>> GetDoctorsByDepartmentIdAsync(int departmentId);

        /// <summary>
        /// Adds a new doctor to the system.
        /// </summary>
        /// <param name="doctor">The doctor to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddDoctorAsync(Doctor doctor);

        /// <summary>
        /// Deletes a doctor by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the doctor to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteDoctorAsync(int id);
    }
}
