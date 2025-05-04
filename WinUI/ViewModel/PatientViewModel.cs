using System;
using System.ComponentModel;
using System.Threading.Tasks;
using WinUI.Model;
using WinUI.Service;

namespace WinUI.ViewModel
{
    public class PatientViewModel : IPatientViewModel
    {
        private readonly IPatientService _patientManager;
        public event PropertyChangedEventHandler? PropertyChanged;

        public PatientJointModel _originalPatient { get; private set; }

        public PatientViewModel(IPatientService patientManager, int userId)
        {
            _patientManager = patientManager;
            _userId = userId;
            _originalPatient = PatientJointModel.Default;
            _ = LoadPatientInfoByUserIdAsync(userId);
        }

        private int _userId;
        public int UserId
        {
            get => _userId;
            set { if (_userId != value) { _userId = value; OnPropertyChanged(); } }
        }

        // These properties stay for binding/display
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string BloodType { get; set; } = string.Empty;
        public string Allergies { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string Cnp { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; }

        private string _emergencyContact = string.Empty;
        public string EmergencyContact
        {
            get => _emergencyContact;
            set { if (_emergencyContact != value) { _emergencyContact = value; OnPropertyChanged(); } }
        }

        private double _weight;
        public double Weight
        {
            get => _weight;
            set { if (_weight != value) { _weight = value; OnPropertyChanged(); } }
        }

        private int _height;
        public int Height
        {
            get => _height;
            set { if (_height != value) { _height = value; OnPropertyChanged(); } }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set { if (_isLoading != value) { _isLoading = value; OnPropertyChanged(); } }
        }

        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task<bool> LoadPatientInfoByUserIdAsync(int userId)
        {
            try
            {
                IsLoading = true;

                bool success = await _patientManager.LoadPatientInfoByUserId(userId);
                var patient = _patientManager._patientInfo;

                if (success && patient != PatientJointModel.Default)
                {
                    Name = patient.PatientName;
                    Email = patient.Email;
                    Username = patient.Username;
                    Address = patient.Address;
                    PhoneNumber = patient.PhoneNumber;
                    EmergencyContact = patient.EmergencyContact;
                    BloodType = patient.BloodType;
                    Allergies = patient.Allergies;
                    BirthDate = patient.BirthDate.ToDateTime(TimeOnly.MinValue);
                    Cnp = patient.CNP;
                    RegistrationDate = patient.RegistrationDate;
                    Weight = patient.Weight;
                    Height = patient.Height;

                    _originalPatient = new PatientJointModel(
                        userId,
                        patient.PatientId,
                        Name,
                        BloodType,
                        EmergencyContact,
                        Allergies,
                        Weight,
                        Height,
                        Username,
                        "", // password unused now
                        Email,
                        patient.BirthDate,
                        Cnp,
                        Address,
                        PhoneNumber,
                        RegistrationDate
                    );
                }

                IsLoading = false;
                return success;
            }
            catch (Exception exception)
            {
                IsLoading = false;
                Console.WriteLine($"Error loading patient info: {exception.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateEmergencyContact(string emergencyContact)
        {
            try
            {
                IsLoading = true;
                bool updated = await _patientManager.UpdateEmergencyContact(UserId, emergencyContact);
                if (updated)
                {
                    EmergencyContact = emergencyContact;
                    _originalPatient.EmergencyContact = emergencyContact;
                }
                return updated;
            }
            finally { IsLoading = false; }
        }

        public async Task<bool> UpdateWeight(double weight)
        {
            try
            {
                IsLoading = true;
                bool updated = await _patientManager.UpdateWeight(UserId, weight);
                if (updated)
                {
                    Weight = weight;
                    _originalPatient.Weight = weight;
                }
                return updated;
            }
            finally { IsLoading = false; }
        }

        public async Task<bool> UpdateHeight(int height)
        {
            try
            {
                IsLoading = true;
                bool updated = await _patientManager.UpdateHeight(UserId, height);
                if (updated)
                {
                    Height = height;
                    _originalPatient.Height = height;
                }
                return updated;
            }
            finally { IsLoading = false; }
        }

        public async Task<bool> LogUpdate(int userId, ActionType action)
        {
            return await _patientManager.LogUpdate(userId, action);
        }
    }
}
