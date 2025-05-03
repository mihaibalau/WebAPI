using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.IRepository;
using Domain;
using WinUI.Model;
using WinUI.Service;

namespace WinUI.Service
{
    class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository; // ← From ClassLibrary.IRepository

        public PatientJointModel _patientInfo { get; private set; } = PatientJointModel.Default;
        public List<PatientJointModel> _patientList { get; private set; } = new List<PatientJointModel>();

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<bool> LoadPatientInfoByUserId(int userId)
        {
            var domainPatient = await _patientRepository.GetPatientByUserIdAsync(userId);
            if (domainPatient == null)
            {
                _patientInfo = PatientJointModel.Default;
                return false;
            }

            _patientInfo = MapToJointModel(domainPatient);
            Debug.WriteLine($"Patient info loaded: {_patientInfo.PatientName}");
            return true;
        }

        public async Task<bool> LoadAllPatients()
        {
            var domainPatients = await _patientRepository.GetAllPatientsAsync();
            _patientList = domainPatients.Select(MapToJointModel).ToList();
            return true;
        }

        public virtual async Task<bool> UpdatePassword(int userId, string password)
        {
            throw new NotSupportedException("UpdatePassword is not supported in IPatientRepository from ClassLibrary.");
        }

        public virtual async Task<bool> UpdateEmail(int userId, string email)
        {
            throw new NotSupportedException("UpdateEmail is not supported in IPatientRepository from ClassLibrary.");
        }

        public virtual async Task<bool> UpdateUsername(int userId, string username)
        {
            throw new NotSupportedException("UpdateUsername is not supported in IPatientRepository from ClassLibrary.");
        }

        public virtual async Task<bool> UpdateName(int userId, string name)
        {
            throw new NotSupportedException("UpdateName is not supported in IPatientRepository from ClassLibrary.");
        }

        public virtual async Task<bool> UpdateBirthDate(int userId, DateOnly birthDate)
        {
            throw new NotSupportedException("UpdateBirthDate is not supported in IPatientRepository from ClassLibrary.");
        }

        public virtual async Task<bool> UpdateAddress(int userId, string address)
        {
            throw new NotSupportedException("UpdateAddress is not supported in IPatientRepository from ClassLibrary.");
        }

        public virtual async Task<bool> UpdatePhoneNumber(int userId, string phoneNumber)
        {
            throw new NotSupportedException("UpdatePhoneNumber is not supported in IPatientRepository from ClassLibrary.");
        }

        public virtual async Task<bool> UpdateEmergencyContact(int userId, string emergencyContact)
        {
            // This one is supported
            var domainPatient = await _patientRepository.GetPatientByUserIdAsync(userId);
            if (domainPatient == null) return false;
            domainPatient.EmergencyContact = emergencyContact;
            await _patientRepository.AddPatientAsync(domainPatient); // ← assumes Add is Upsert
            return true;
        }

        public virtual async Task<bool> UpdateWeight(int userId, double weight)
        {
            var domainPatient = await _patientRepository.GetPatientByUserIdAsync(userId);
            if (domainPatient == null) return false;
            domainPatient.Weight = weight;
            await _patientRepository.AddPatientAsync(domainPatient); // ← assumes Add is Upsert
            return true;
        }

        public virtual async Task<bool> UpdateHeight(int userId, int height)
        {
            var domainPatient = await _patientRepository.GetPatientByUserIdAsync(userId);
            if (domainPatient == null) return false;
            domainPatient.Height = height;
            await _patientRepository.AddPatientAsync(domainPatient); // ← assumes Add is Upsert
            return true;
        }

        public virtual async Task<bool> LogUpdate(int userId, ActionType action)
        {
            Debug.WriteLine($"LogUpdate called for user {userId}, action: {action}");
            return await Task.FromResult(true); // stub implementation
        }

        private PatientJointModel MapToJointModel(Patient domainPatient)
        {
            return new PatientJointModel(
                domainPatient.UserId,
                0, // no PatientId in Domain.Patient
                "", // no PatientName in Domain.Patient
                domainPatient.BloodType,
                domainPatient.EmergencyContact,
                domainPatient.Allergies,
                domainPatient.Weight,
                domainPatient.Height,
                "", "", "", // Username, Password, Email missing
                DateOnly.MinValue, "", "", "", DateTime.MinValue
            );
        }
    }
}
