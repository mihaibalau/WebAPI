using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.IRepository
{
    public interface IPatientRepository
    {
        /// <summary>
        /// Get all patients from the database
        /// </summary>
        /// <returns>The list of all patients in the database.</returns>
        Task<List<Patient>> GetAllPatientsAsync();

        /// <summary>
        /// Get a patient by id
        /// </summary>
        /// <param name="id"> The id of the patient to get.</param>
        /// <returns> The patient with the given id.</returns>
        Task<Patient> GetPatientByUserIdAsync(int id);

        /// <summary>
        /// Get a patient by name
        /// </summary>
        /// <param name="patient"> The patient to get.</param>
        /// <returns>A task representing the asynchronous operation..</returns>
        Task AddPatientAsync(Patient patient);

        /// <summary>
        /// Delete a patient in the database
        /// </summary>
        /// <param name="patient"> The patient to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeletePatientAsync(int id);
    }
}
