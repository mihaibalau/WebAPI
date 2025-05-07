using ClassLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinUI.Model;

namespace WinUI.Repository
{
    public interface IPatientRepository
    {
        /*Task<List<PatientJointModel>> getAllPatients();
        Task<PatientJointModel> getPatientByUserId(int userId);
        Task<bool> updatePassword(int userId, string password);
        Task<bool> updateEmail(int userId, string email);
        Task<bool> updateUsername(int userId, string username);
        Task<bool> updateName(int userId, string name);
        Task<bool> updateBirthDate(int userId, DateOnly birthDate);
        Task<bool> updateAddress(int userId, string address);
        Task<bool> updatePhoneNumber(int userId, string phoneNumber);
        Task<bool> updateEmergencyContact(int userId, string emergencyContact);
        Task<bool> updateWeight(int userId, double weight);
        Task<bool> updateHeight(int userId, int height);
        Task<bool> logUpdate(int userId, ActionType type);*/
        /// <summary>
        /// Get all patients from the database
        /// </summary>
        /// <returns>The list of all patients in the database.</returns>
        Task<List<Patient>> getAllPatientsAsync();

        /// <summary>
        /// Get a patient by id
        /// </summary>
        /// <param name="id"> The id of the patient to get.</param>
        /// <returns> The patient with the given id.</returns>
        Task<Patient> getPatientByUserIdAsync(int id);

        Task<List<User>> getAllUserAsync();

        /// <summary>
        /// Get a patient by name
        /// </summary>
        /// <param name="patient"> The patient to get.</param>
        /// <returns>A task representing the asynchronous operation..</returns>
        Task addPatientAsync(Patient patient);

        Task updatePatientAsync(Patient patient, User user);

        /// <summary>
        /// Delete a patient in the database
        /// </summary>
        /// <param name="patient"> The patient to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task deletePatientAsync(int id);
    }
}
