using System;
using System.ComponentModel;
using System.Threading.Tasks;
using WinUI.Model;
using WinUI.Service;

namespace WinUI.ViewModel
{
    public class PatientViewModel : IPatientViewModel
    {
        private readonly IPatientService _patient_service;
        public event PropertyChangedEventHandler? PropertyChanged;
        public string password { get; set; } = string.Empty;

        public PatientJointModel _original_patient { get; private set; }

        public PatientViewModel(IPatientService patient_service, int user_id)
        {
            _patient_service = patient_service;
            _user_id = user_id;
            _original_patient = PatientJointModel.Default;
            _ = loadPatientInfoByUserIdAsync(_user_id);
        }

        private int _user_id;
        public int user_id
        {
            get => this._user_id;
            set { if (this._user_id != value) { this._user_id = value; OnPropertyChanged(); } }
        }

        // These properties stay for binding/display
        private string _name = string.Empty;
        public string name
        {
            get => this._name;
            set { if (this._name != value) { this._name = value; OnPropertyChanged(); } }
        }

        private string _email = string.Empty;
        public string email
        {
            get => this._email;
            set { if (this._email != value) { this._email = value; OnPropertyChanged(); } }
        }
        private string _username = string.Empty;
        public string username
        {
            get => this._username;
            set { if (this._username != value) { this._username = value; OnPropertyChanged(); } }
        }

        private string _address = string.Empty;
        public string address
        {
            get => this._address;
            set { if (this._address != value) { this._address = value; OnPropertyChanged(); } }
        }
        private string _phone_number = string.Empty;
        public string phone_number
        {
            get => this._phone_number;
            set { if (this._phone_number != value) { this._phone_number = value; OnPropertyChanged(); } }
        }
        private string _blood_type = string.Empty;
        public string blood_type
        {
            get => this._blood_type;
            set { if (this._blood_type != value) { this._blood_type = value; OnPropertyChanged(); } }
        }
        private string _allergies = string.Empty;
        public string allergies
        {
            get => this._allergies;
            set { if (this._allergies != value) { this._allergies = value; OnPropertyChanged(); } }
        }
        private DateOnly _birth_date;
        public DateOnly birth_date
        {
            get => this._birth_date;
            set { if (this._birth_date != value) { this._birth_date = value; OnPropertyChanged(); } }
        }
        private string _cnp = string.Empty;
        public string cnp
        {
            get => this._cnp;
            set { if (this._cnp != value) { this._cnp = value; OnPropertyChanged(); } }
        }
        private DateTime _registration_date;
        public DateTime registration_date
        {
            get => this._registration_date;
            set { if (this._registration_date != value) { this._registration_date = value; OnPropertyChanged(); } }
        }

        private string _emergencyContact = string.Empty;
        public string emergency_contact
        {
            get => this._emergencyContact;
            set { if (this._emergencyContact != value) { this._emergencyContact = value; OnPropertyChanged(); } }
        }

        private double _weight;
        public double weight
        {
            get => this._weight;
            set { if (this._weight != value) { this._weight = value; OnPropertyChanged(); } }
        }

        private int _height;
        public int height
        {
            get => this._height;
            set { if (this._height != value) { this._height = value; OnPropertyChanged(); } }
        }

        private bool _is_loading;
        public bool is_loading
        {
            get => this._is_loading;
            set { if (this._is_loading != value) { this._is_loading = value; OnPropertyChanged(); } }
        }

        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task<bool> loadPatientInfoByUserIdAsync(int _user_id)
        {
            try
            {
                this.is_loading = true;

                bool success = await this._patient_service.loadPatientInfoByUserId(_user_id);
                PatientJointModel? patient = this._patient_service.patientInfo;

                if (success && patient != PatientJointModel.Default)
                {
                    this.name = patient.patientName;
                    this.email = patient.email;
                    this.username = patient.username;
                    this.address = patient.address;
                    this.phone_number = patient.phoneNumber;
                    this.emergency_contact = patient.emergencyContact;
                    this.blood_type = patient.bloodType;
                    this.allergies = patient.allergies;
                    this.birth_date = patient.birthDate;
                    this.cnp = patient.cnp;
                    this.registration_date = patient.registrationDate;
                    this.weight = patient.weight;
                    this.height = patient.height;

                    this._original_patient = new PatientJointModel(
                        this._user_id,
                        patient.patientId,
                        this.name,
                        this.blood_type,
                        this.emergency_contact,
                        this.allergies,
                        this.weight,
                        this.height,
                        this.username,
                        "", // password unused now
                        this.email,
                        patient.birthDate,
                        this.cnp,
                        this.address,
                        this.phone_number,
                        this.registration_date
                    );
                }

                this.is_loading = false;
                return success;
            }
            catch (Exception exception)
            {
                this.is_loading = false;
                Console.WriteLine($"Error loading patient info: {exception.Message}");
                return false;
            }
        }

        public async Task<bool> updateEmergencyContact(string _emergencyContact)
        {
            try
            {
                this.is_loading = true;
                bool updated = await this._patient_service.updateEmergencyContact(user_id, _emergencyContact);
                if (updated)
                {
                    this.emergency_contact = _emergencyContact;
                    this._original_patient.emergencyContact = _emergencyContact;
                }
                return updated;
            }
            finally { this.is_loading = false; }
        }

        public async Task<bool> updateWeight(double _weight)
        {
            try
            {
                this.is_loading = true;
                bool updated = await this._patient_service.updateWeight(user_id, _weight);
                if (updated)
                {
                    this.weight = weight;
                    this._original_patient.weight = weight;
                }
                return updated;
            }
            finally { this.is_loading = false; }
        }

        public async Task<bool> updateHeight(int _height)
        {
            try
            {
                this.is_loading = true;
                bool updated = await this._patient_service.updateHeight(user_id, _height);
                if (updated)
                {
                    this.height = _height;
                    this._original_patient.height = _height;
                }
                return updated;
            }
            finally { this.is_loading = false; }
        }

        public async Task<bool> updatePassword(string _password)
        {
            try
            {
                this.is_loading = true;
                bool updated = await this._patient_service.updatePassword(user_id, _password);
                if (updated)
                {
                    this.password = _password;
                    this._original_patient.password = _password;
                }
                return updated;
            }
            finally { this.is_loading = false; }
        }

        public async Task<bool> updateName(string _name)
        {
            try
            {
                this.is_loading = true;
                bool updated = await this._patient_service.updateName(user_id, _name);
                if (updated)
                {
                    this.name = _name;
                    this._original_patient.patientName = _name;
                }
                return updated;
            }
            finally { this.is_loading = false; }
        }

        public async Task<bool> updateAddress(string _address)
        {
            try
            {
                this.is_loading = true;
                bool updated = await this._patient_service.updateAddress(user_id, _address);
                if (updated)
                {
                    this.address = _address;
                    this._original_patient.address = _address;
                }
                return updated;
            }
            finally { this.is_loading = false; }
        }

        public async Task<bool> updatePhoneNumber(string _phone_number)
        {
            try
            {
                this.is_loading = true;
                bool updated = await this._patient_service.updatePhoneNumber(user_id, _phone_number);
                if (updated)
                {
                    this.phone_number = _phone_number;
                    this._original_patient.phoneNumber = _phone_number;
                }
                return updated;
            }
            finally { this.is_loading = false; }
        }

        public async Task<bool> logUpdate(int _user_id, ActionType _action)
        {
            return await this._patient_service.logUpdate(_user_id, _action);
        }
    }
}
