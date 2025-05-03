using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WinUI.Model;

namespace WinUI.Service
{
    public interface IPatientService
    {
        PatientJointModel _patientInfo { get; }
        List<PatientJointModel> _patientList { get; }

        Task<bool> LoadPatientInfoByUserId(int userId);
        Task<bool> LoadAllPatients();

        Task<bool> UpdateWeight(int userId, double weight);
        Task<bool> UpdateHeight(int userId, int height);
        Task<bool> UpdateEmergencyContact(int userId, string emergencyContact);

        Task<bool> LogUpdate(int userId, ActionType action);
    }
}
