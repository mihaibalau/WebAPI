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
        public string name { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string username { get; set; } = string.Empty;
        public string address { get; set; } = string.Empty;
        public string phone_number { get; set; } = string.Empty;
        public string blood_type { get; set; } = string.Empty;
        public string allergies { get; set; } = string.Empty;
        public DateTime birth_date { get; set; }
        public string cnp { get; set; } = string.Empty;
        public DateTime registration_date { get; set; }

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
                PatientJointModel? patient = this._patient_service._patient_info;

                if (success && patient != PatientJointModel.Default)
                {
                    this.name = patient.patient_name;
                    this.email = patient.email;
                    this.username = patient.username;
                    this.address = patient.address;
                    this.phone_number = patient.phone_number;
                    this.emergency_contact = patient.emergency_contact;
                    this.blood_type = patient.blood_type;
                    this.allergies = patient.allergies;
                    this.birth_date = patient.birth_date.ToDateTime(TimeOnly.MinValue);
                    this.cnp = patient.cnp;
                    this.registration_date = patient.registration_date;
                    this.weight = patient.weight;
                    this.height = patient.height;

                    this._original_patient = new PatientJointModel(
                        this._user_id,
                        patient.patient_id,
                        this.name,
                        this.blood_type,
                        this.emergency_contact,
                        this.allergies,
                        this.weight,
                        this.height,
                        this.username,
                        "", // password unused now
                        this.email,
                        patient.birth_date,
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
                    this._original_patient.emergency_contact = _emergencyContact;
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

        public async Task<bool> logUpdate(int _user_id, ActionType _action)
        {
            return await this._patient_service.logUpdate(_user_id, _action);
        }
    }
}
