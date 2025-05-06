using System;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using WinUI.Model;
using WinUI.ViewModel;

namespace WinUI.View
{
    public sealed partial class PatientDashboardControl : UserControl
    {
        private IPatientViewModel? _patient_view_model;
        public event Action? LogoutButtonClicked;

        public PatientDashboardControl()
        {
            this.InitializeComponent();
        }

        public PatientDashboardControl(IPatientViewModel _patient_view_model)
        {
            InitializeComponent();
            this._patient_view_model = _patient_view_model;
            DataContext = this._patient_view_model;
        }

        private async void OnUpdateButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this._patient_view_model == null)
                    throw new Exception("Patient is not initialized");

                bool has_changes = false;

                if (this._patient_view_model.emergency_contact != this._patient_view_model._original_patient.emergency_contact)
                {
                    bool emergencyUpdated = await this._patient_view_model.updateEmergencyContact(this._patient_view_model.emergency_contact);
                    has_changes |= emergencyUpdated;
                }

                if (this._patient_view_model.weight != this._patient_view_model._original_patient.weight)
                {
                    bool weight_updated = await this._patient_view_model.updateWeight(this._patient_view_model.weight);
                    has_changes |= weight_updated;
                }

                if (this._patient_view_model.height != this._patient_view_model._original_patient.height)
                {
                    bool height_updated = await this._patient_view_model.updateHeight(this._patient_view_model.height);
                    has_changes |= height_updated;
                }

                if (has_changes)
                {
                    await this._patient_view_model.logUpdate(this._patient_view_model.user_id, ActionType.UPDATE_PROFILE);
                    await ShowDialogAsync("Success", "Changes applied successfully.");
                }
                else
                {
                    await ShowDialogAsync("No Changes", "Please modify the fields you want to update.");
                }
            }
            catch (Exception exception)
            {
                if (this._patient_view_model != null)
                {
                    restoreOriginalPatientData();
                    await ShowDialogAsync("Error", exception.Message);
                    await this._patient_view_model.loadPatientInfoByUserIdAsync(this._patient_view_model.user_id);
                }
            }
        }

        private void restoreOriginalPatientData()
        {
            PatientJointModel? original = this._patient_view_model!._original_patient;
            this._patient_view_model!.emergency_contact = original.emergency_contact;
            this._patient_view_model.weight = original.weight;
            this._patient_view_model.height = original.height;
        }

        private async Task ShowDialogAsync(string _title, string _message)
        {
            var dialog = new ContentDialog
            {
                Title = _title,
                Content = _message,
                CloseButtonText = "OK",
                XamlRoot = this.Content.XamlRoot
            };

            await dialog.ShowAsync();
        }

        private void OnLogoutButtonClick(object sender, RoutedEventArgs e)
        {
            LogoutButtonClicked?.Invoke();
        }
    }
}
