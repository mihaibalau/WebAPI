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
        private IPatientViewModel? _patientViewModel;
        public event Action? LogoutButtonClicked;

        public PatientDashboardControl()
        {
            this.InitializeComponent();
        }

        public PatientDashboardControl(IPatientViewModel patientViewModel)
        {
            InitializeComponent();
            _patientViewModel = patientViewModel;
            DataContext = _patientViewModel;
        }

        private async void OnUpdateButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_patientViewModel == null)
                    throw new Exception("Patient is not initialized");

                bool hasChanges = false;

                if (_patientViewModel.EmergencyContact != _patientViewModel._originalPatient.EmergencyContact)
                {
                    bool emergencyUpdated = await _patientViewModel.UpdateEmergencyContact(_patientViewModel.EmergencyContact);
                    hasChanges |= emergencyUpdated;
                }

                if (_patientViewModel.Weight != _patientViewModel._originalPatient.Weight)
                {
                    bool weightUpdated = await _patientViewModel.UpdateWeight(_patientViewModel.Weight);
                    hasChanges |= weightUpdated;
                }

                if (_patientViewModel.Height != _patientViewModel._originalPatient.Height)
                {
                    bool heightUpdated = await _patientViewModel.UpdateHeight(_patientViewModel.Height);
                    hasChanges |= heightUpdated;
                }

                if (hasChanges)
                {
                    await _patientViewModel.LogUpdate(_patientViewModel.UserId, ActionType.UPDATE_PROFILE);
                    await ShowDialogAsync("Success", "Changes applied successfully.");
                }
                else
                {
                    await ShowDialogAsync("No Changes", "Please modify the fields you want to update.");
                }
            }
            catch (Exception exception)
            {
                if (_patientViewModel != null)
                {
                    RestoreOriginalPatientData();
                    await ShowDialogAsync("Error", exception.Message);
                    await _patientViewModel.LoadPatientInfoByUserIdAsync(_patientViewModel.UserId);
                }
            }
        }

        private void RestoreOriginalPatientData()
        {
            var original = _patientViewModel!._originalPatient;
            _patientViewModel!.EmergencyContact = original.EmergencyContact;
            _patientViewModel.Weight = original.Weight;
            _patientViewModel.Height = original.Height;
        }

        private async Task ShowDialogAsync(string title, string message)
        {
            var dialog = new ContentDialog
            {
                Title = title,
                Content = message,
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
