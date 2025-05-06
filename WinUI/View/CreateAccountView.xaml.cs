using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinUI.Exceptions;
using WinUI.Model;
using WinUI.Service;
using WinUI.ViewModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUI.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateAccountView : Page
    {
        private IAuthViewModel _view_model_create_account;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAccountView"/> class.
        /// </summary>
        public CreateAccountView()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAccountView"/> class.
        /// </summary>
        /// <param name="_auth_view_model">View Model for Creating an account.</param>
        public CreateAccountView(AuthViewModel _auth_view_model)
        {
            this.InitializeComponent();
            this._view_model_create_account = _auth_view_model;
        }

        /// <summary>
        /// Handle navigation parameters
        /// </summary>
        protected override void OnNavigatedTo(NavigationEventArgs _navigate_event_args)
        {
            base.OnNavigatedTo(_navigate_event_args);

            if (_navigate_event_args.Parameter is AuthViewModel _auth_view_model)
            {
                this._view_model_create_account = _auth_view_model;
            }
        }

        private async void createAccountButtonClick(object _sender, RoutedEventArgs _routed_event_args)
        {
            string _username = this.username_field.Text;
            string _password = this.password_field.Password;
            string _mail = this.email_text_box.Text;
            string _name = this.name_text_box.Text;
            string _emergencyContact = this.emergency_contact_text_box.Text;

            if (this.birth_date_calendar_picker.Date.HasValue)
            {
                DateOnly _birth_date = DateOnly.FromDateTime(this.birth_date_calendar_picker.Date.Value.DateTime);
                this.birth_date_calendar_picker.Date = new DateTimeOffset(_birth_date.ToDateTime(TimeOnly.MinValue));

                string _cnp = this._cnp_textbox.Text;

                BloodType? _selected_blood_type = null;
                if (this.blood_type_combo_box.SelectedItem is ComboBoxItem _selected_item)
                {
                    string? _selectedTag = _selected_item.Tag.ToString();
                    if (_selectedTag != null)
                    {
                        switch (_selectedTag.Trim())
                        {
                            case "A_POSITIVE":
                                _selected_blood_type = BloodType.A_POSITIVE;
                                break;
                            case "A_NEGATIVE":
                                _selected_blood_type = BloodType.A_NEGATIVE;
                                break;
                            case "B_POSITIVE":
                                _selected_blood_type = BloodType.B_POSITIVE;
                                break;
                            case "B_NEGATIVE":
                                _selected_blood_type = BloodType.B_NEGATIVE;
                                break;
                            case "AB_POSITIVE":
                                _selected_blood_type = BloodType.AB_POSITIVE;
                                break;
                            case "AB_NEGATIVE":
                                _selected_blood_type = BloodType.AB_NEGATIVE;
                                break;
                            case "O_POSITIVE":
                                _selected_blood_type = BloodType.O_POSITIVE;
                                break;
                            case "O_NEGATIVE":
                                _selected_blood_type = BloodType.O_NEGATIVE;
                                break;
                        }
                    }
                }

                if (_selected_blood_type == null)
                {
                    var _validation_dialog = new ContentDialog
                    {
                        Title = "Error",
                        Content = "Please select a blood type.",
                        CloseButtonText = "OK",
                    };

                    _validation_dialog.XamlRoot = this.Content.XamlRoot;
                    await _validation_dialog.ShowAsync();
                    return;
                }

                bool _weight_valid = double.TryParse(this.weight_text_box.Text, out double _weight);
                bool _height_valid = int.TryParse(this.height_text_box.Text, out int _height);

                if (!_weight_valid || !_height_valid || _weight <= 0 || _height <= 0)
                {
                    var _validation_dialog = new ContentDialog
                    {
                        Title = "Error",
                        Content = "Please enter valid Weight (kg) and Height (cm).",
                        CloseButtonText = "OK",
                    };

                    _validation_dialog.XamlRoot = this.Content.XamlRoot;
                    await _validation_dialog.ShowAsync();
                    return;
                }

                try
                {
                    await this._view_model_create_account.createAccount(new UserCreateAccountModel(_username, _password, _mail, _name, _birth_date, _cnp, (BloodType)_selected_blood_type, _emergencyContact, _weight, _height));

                    //PatientService patientService = new PatientService();
                    //PatientViewModel patientViewModel = new PatientViewModel(patientService, this._view_model_create_account.AuthService.allUserInformation.UserId);
                    //// Navigate to PatientDashboardPage
                    //if (App.MainWindow is LoginWindow loginWindow)
                    //{
                    //    var parameters = new Tuple<IPatientViewModel, IAuthViewModel>(patientViewModel, this._view_model_create_account);
                    //    loginWindow.ReturnToLogin();
                    //    // Optionally navigate to patient dashboard if auto-login is desired
                    //    // loginWindow.mainFrame.Navigate(typeof(PatientDashboardPage), parameters);
                    //}

                    var _validation_dialog = new ContentDialog
                    {
                        Title = "Success!",
                        Content = "Successfully Registered",
                        CloseButtonText = "OK",
                    };

                    _validation_dialog.XamlRoot = this.Content.XamlRoot;
                    await _validation_dialog.ShowAsync();
                    return;

                }
                catch (AuthenticationException err)
                {
                    var _validation_dialog = new ContentDialog
                    {
                        Title = "Error",
                        Content = $"{err.Message}",
                        CloseButtonText = "OK",
                        XamlRoot = this.Content.XamlRoot,
                    };
                    await _validation_dialog.ShowAsync();
                }
            }
            else
            {
                var _validation_dialog = new ContentDialog
                {
                    Title = "Error",
                    Content = "Birth date is required.",
                    CloseButtonText = "OK",
                };

                _validation_dialog.XamlRoot = this.Content.XamlRoot;
                await _validation_dialog.ShowAsync();
            }
        }

        private void goBackButtonClick(object _sender, RoutedEventArgs _routed_event_args)
        {
            NavigationService.navigateToLogin();
        }
    }
}
