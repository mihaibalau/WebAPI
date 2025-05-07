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
            User filtered_user = domain_users.Find(user => user.userId == _user_id);
            if (domain_patient == null || filtered_user == null)
            {
                _patient_info = PatientJointModel.Default;
                return false;
            }

            this._patient_info = mapToJointModel(domain_patient, filtered_user);
            Debug.WriteLine($"Patient info loaded: {this._patient_info.patientName}");
            return true;
        }

        public async Task<bool> loadAllPatients()
        {
            List<Patient> domain_patients = await this._patient_repository.getAllPatientsAsync();
            List<User> domain_users = await this._patient_repository.getAllUserAsync();

            this._patient_list.Clear();
            foreach (var patient in domain_patients)
            {
                User? matched_user = domain_users.Find(user => user.userId == patient.userId);
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
            User filtered_user = domain_users.Find(user => user.userId == _user_id);
            if (domain_patient == null || filtered_user == null)
            {
                _patient_info = PatientJointModel.Default;
                return false;
            }

            filtered_user.password = _password;
            await this._patient_repository.updatePatientAsync(domain_patient, filtered_user);
            return true;
        }

        public virtual async Task<bool> updateName(int _user_id, string _name)
        {
            Patient domain_patient = await _patient_repository.getPatientByUserIdAsync(_user_id);
            List<User> domain_users = await this._patient_repository.getAllUserAsync();
            User filtered_user = domain_users.Find(user => user.userId == _user_id);
            if (domain_patient == null || filtered_user == null)
            {
                _patient_info = PatientJointModel.Default;
                return false;
            }

            filtered_user.name = _name;
            await this._patient_repository.updatePatientAsync(domain_patient, filtered_user);
            return true;
        }

        public virtual async Task<bool> updateAddress(int _user_id, string _address)
        {
            Patient domain_patient = await _patient_repository.getPatientByUserIdAsync(_user_id);
            List<User> domain_users = await this._patient_repository.getAllUserAsync();
            User filtered_user = domain_users.Find(user => user.userId == _user_id);
            if (domain_patient == null || filtered_user == null)
            {
                _patient_info = PatientJointModel.Default;
                return false;
            }

            filtered_user.address = _address;
            await this._patient_repository.updatePatientAsync(domain_patient, filtered_user);
            return true;
        }

        public virtual async Task<bool> updatePhoneNumber(int _user_id, string _phone_number)
        {
            Patient domain_patient = await _patient_repository.getPatientByUserIdAsync(_user_id);
            List<User> domain_users = await this._patient_repository.getAllUserAsync();
            User filtered_user = domain_users.Find(user => user.userId == _user_id);
            if (domain_patient == null || filtered_user == null)
            {
                _patient_info = PatientJointModel.Default;
                return false;
            }

            filtered_user.phoneNumber = _phone_number;
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
            domain_patient.weight = weight;
            await this._patient_repository.addPatientAsync(domain_patient);
            return true;
        }

        public virtual async Task<bool> updateHeight(int _user_id, int height)
        {
            Patient domain_patient = await this._patient_repository.getPatientByUserIdAsync(_user_id);
            if (domain_patient == null) return false;
            domain_patient.height = height;
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
                _domain_patient.userId,
                _domain_user.userId,
                _domain_user.name,
                _domain_patient.bloodType ?? string.Empty,
                _domain_patient.EmergencyContact ?? string.Empty,
                _domain_patient.allergies ?? string.Empty,
                _domain_patient.weight,
                _domain_patient.height,
                _domain_user.username,
                _domain_user.password,
                _domain_user.mail,
                _domain_user.birthDate,
                _domain_user.cnp ?? string.Empty,
                _domain_user.address ?? string.Empty,
                _domain_user.phoneNumber ?? string.Empty,
                _domain_user.registrationDate
            );
        }
    }
}
