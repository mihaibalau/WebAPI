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
        Task<List<PatientJointModel>> getAllPatients();
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
        Task<bool> logUpdate(int userId, ActionType type);
    }
}
