using System;
using System.ComponentModel;
using System.Threading.Tasks;
using WinUI.Model;

namespace WinUI.ViewModel
{
    public interface IPatientViewModel : INotifyPropertyChanged
    {
        int user_id { get; set; }
        string name { get; set; }
        string email { get; set; }
        string username { get; set; }
        string password { get; set; }
        string address { get; set; }
        string phone_number { get; set; }
        string emergency_contact { get; set; }
        string blood_type { get; set; }
        string allergies { get; set; }
        DateTime birth_date { get; set; }
        string cnp { get; set; }
        DateTime registration_date { get; set; }
        double weight { get; set; }
        int height { get; set; }
        bool is_loading { get; set; }

        PatientJointModel _original_patient { get; }

        Task<bool> loadPatientInfoByUserIdAsync(int _user_id);
        Task<bool> updateEmergencyContact(string _emergency_contact);
        Task<bool> updateWeight(double _weight);
        Task<bool> updateHeight(int _height);

        Task<bool> logUpdate(int _user_id, ActionType _action);
    }
}
