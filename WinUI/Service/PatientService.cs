using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinUI.Repository;
using ClassLibrary.Domain;
using WinUI.Model;
using WinUI.Service;
using static WinUI.Proxy.LogInProxy;
using Windows.System;
using User = ClassLibrary.Domain.User;
using System.Net;

namespace WinUI.Service
{
    class PatientService : IPatientService
    {
        private readonly IPatientRepository _patient_repository; // ← From ClassLibrary.IRepository

        public PatientJointModel _patient_info { get; private set; } = PatientJointModel.Default;
        public List<PatientJointModel> _patient_list { get; private set; } = new List<PatientJointModel>();

        public PatientService(IPatientRepository _patient_repository)
        {
            this._patient_repository = _patient_repository;
        }

        public async Task<bool> loadPatientInfoByUserId(int _user_id)
        {
            Patient domain_patient = await this._patient_repository.getPatientByUserIdAsync(_user_id);
            List<User> domain_users = await this._patient_repository.getAllUserAsync();
            User filtered_user = domain_users.Find(user => user.UserId == _user_id);
            if (domain_patient == null || filtered_user == null)
            {
                _patient_info = PatientJointModel.Default;
                return false;
            }

            this._patient_info = mapToJointModel(domain_patient, filtered_user);
            Debug.WriteLine($"Patient info loaded: {this._patient_info.patient_name}");
            return true;
        }

        public async Task<bool> loadAllPatients()
        {
            List<Patient> domain_patients = await this._patient_repository.getAllPatientsAsync();
            List<User> domain_users = await this._patient_repository.getAllUserAsync();

            this._patient_list.Clear();
            foreach (var patient in domain_patients)
            {
                User? matched_user = domain_users.Find(user => user.UserId == patient.UserId);
                if (matched_user != null)
                {
                    this._patient_list.Add(mapToJointModel(patient, matched_user));
                }
            }
            return true;
        }

        public virtual async Task<bool> updatePassword(int _user_id, string _password)
        {
            Patient domain_patient = await _patient_repository.getPatientByUserIdAsync(_user_id);
            List<User> domain_users = await this._patient_repository.getAllUserAsync();
            User filtered_user = domain_users.Find(user => user.UserId == _user_id);
            if (domain_patient == null || filtered_user == null)
            {
                _patient_info = PatientJointModel.Default;
                return false;
            }

            filtered_user.Password = _password;
            await this._patient_repository.updatePatientAsync(domain_patient, filtered_user);
            return true;
        }

        public virtual async Task<bool> updateName(int _user_id, string _name)
        {
            Patient domain_patient = await _patient_repository.getPatientByUserIdAsync(_user_id);
            List<User> domain_users = await this._patient_repository.getAllUserAsync();
            User filtered_user = domain_users.Find(user => user.UserId == _user_id);
            if (domain_patient == null || filtered_user == null)
            {
                _patient_info = PatientJointModel.Default;
                return false;
            }

            filtered_user.Name = _name;
            await this._patient_repository.updatePatientAsync(domain_patient, filtered_user);
            return true;
        }

        public virtual async Task<bool> updateAddress(int _user_id, string _address)
        {
            Patient domain_patient = await _patient_repository.getPatientByUserIdAsync(_user_id);
            List<User> domain_users = await this._patient_repository.getAllUserAsync();
            User filtered_user = domain_users.Find(user => user.UserId == _user_id);
            if (domain_patient == null || filtered_user == null)
            {
                _patient_info = PatientJointModel.Default;
                return false;
            }

            filtered_user.Address = _address;
            await this._patient_repository.updatePatientAsync(domain_patient, filtered_user);
            return true;
        }

        public virtual async Task<bool> updatePhoneNumber(int _user_id, string _phone_number)
        {
            Patient domain_patient = await _patient_repository.getPatientByUserIdAsync(_user_id);
            List<User> domain_users = await this._patient_repository.getAllUserAsync();
            User filtered_user = domain_users.Find(user => user.UserId == _user_id);
            if (domain_patient == null || filtered_user == null)
            {
                _patient_info = PatientJointModel.Default;
                return false;
            }

            filtered_user.PhoneNumber = _phone_number;
            await this._patient_repository.updatePatientAsync(domain_patient, filtered_user);
            return true;
        }

        public virtual async Task<bool> updateEmergencyContact(int _user_id, string _emergency_contact)
        {
            Patient domain_patient = await _patient_repository.getPatientByUserIdAsync(_user_id);
            if (domain_patient == null) return false;
            domain_patient.EmergencyContact = _emergency_contact;
            await this._patient_repository.addPatientAsync(domain_patient);
            return true;
        }

        public virtual async Task<bool> updateWeight(int _user_id, double weight)
        {
            Patient domain_patient = await this._patient_repository.getPatientByUserIdAsync(_user_id);
            if (domain_patient == null) return false;
            domain_patient.Weight = weight;
            await this._patient_repository.addPatientAsync(domain_patient);
            return true;
        }

        public virtual async Task<bool> updateHeight(int _user_id, int height)
        {
            Patient domain_patient = await this._patient_repository.getPatientByUserIdAsync(_user_id);
            if (domain_patient == null) return false;
            domain_patient.Height = height;
            await this._patient_repository.addPatientAsync(domain_patient);
            return true;
        }

        public virtual async Task<bool> logUpdate(int _user_id, ActionType action)
        {
            Debug.WriteLine($"logUpdate called for user {_user_id}, action: {action}");
            return await Task.FromResult(true);
        }

        private PatientJointModel mapToJointModel(Patient _domain_patient, User _domain_user)
        {
            return new PatientJointModel(
                _domain_patient.UserId,
                _domain_user.UserId,
                _domain_user.Name,
                _domain_patient.BloodType ?? string.Empty,
                _domain_patient.EmergencyContact ?? string.Empty,
                _domain_patient.Allergies ?? string.Empty,
                _domain_patient.Weight,
                _domain_patient.Height,
                _domain_user.Username,
                _domain_user.Password,
                _domain_user.Mail,
                _domain_user.BirthDate,
                _domain_user.CNP ?? string.Empty,
                _domain_user.Address ?? string.Empty,
                _domain_user.PhoneNumber ?? string.Empty,
                _domain_user.RegistrationDate
            );
        }
    }
}
