using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using WinUI.Model;
using WinUI.Service;
using Microsoft.UI.Xaml;
using WinUI.Proxy;

namespace WinUI.ViewModel
{
    public class DoctorViewModel : IDoctorViewModel, INotifyPropertyChanged
    {
        private const string DefaultDoctorName = "Loading...";
        private const string DefaultDepartmentName = "Loading department...";
        private const double DefaultRating = 0.0;
        private const string DefaultCareerInfo = "Loading career information...";
        private const string DefaultAvatarUrl = "https://picsum.photos/200";
        private const string DefaultPhoneNumber = "Loading phone...";
        private const string DefaultEmail = "Loading mail...";
        private const string NotSpecified = "Not specified";
        private const string NotAvailable = "N/A";
        private const string NotProvided = "Not provided";
        private const string DefaultNotFoundDoctorName = "Doctor profile not found";

        private const string DefaultErrorDoctorName = "Error loading profile";
        private const string DefaultErrorDepartmentName = "Error";
        private const string DefaultErrorCareerInfo = "Please try again later";

        private readonly IDoctorService _doctorService;
        private readonly DoctorsProxy _doctorProxy;
        private readonly UserProxy _userProxy;

        public DoctorViewModel(IDoctorService doctorService, DoctorsProxy doctorProxy, UserProxy userProxy, int userId)
        {
            _doctorService = doctorService;
            _doctorProxy = doctorProxy;
            _userProxy = userProxy;
            UserId = userId;

            DoctorName = DefaultDoctorName;
            DepartmentName = DefaultDepartmentName;
            Rating = DefaultRating;
            CareerInfo = DefaultCareerInfo;
            AvatarUrl = DefaultAvatarUrl;
            PhoneNumber = DefaultPhoneNumber;
            Mail = DefaultEmail;

            OriginalDoctor = DoctorModel.Default;
            IsLoading = false;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public DoctorModel OriginalDoctor { get; set; }

        private int _userId;

        public int UserId
        {
            get => _userId;
            set
            {
                if (_userId != value)
                {
                    _userId = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _doctorName = string.Empty;

        public string DoctorName
        {
            get => _doctorName;
            set
            {
                if (_doctorName != value)
                {
                    _doctorName = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _departmentId;

        public int DepartmentId
        {
            get => _departmentId;
            set
            {
                if (_departmentId != value)
                {
                    _departmentId = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _departmentName = string.Empty;

        public string DepartmentName
        {
            get => _departmentName;
            set
            {
                if (_departmentName != value)
                {
                    _departmentName = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _rating;

        public double Rating
        {
            get => _rating;
            set
            {
                if (_rating != value)
                {
                    _rating = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _careerInfo = string.Empty;

        public string CareerInfo
        {
            get => _careerInfo;
            set
            {
                if (_careerInfo != value)
                {
                    _careerInfo = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _avatarUrl = string.Empty;

        public string AvatarUrl
        {
            get => _avatarUrl;
            set
            {
                if (_avatarUrl != value)
                {
                    _avatarUrl = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _phoneNumber = string.Empty;

        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (_phoneNumber != value)
                {
                    _phoneNumber = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _mail = string.Empty;

        public string Mail
        {
            get => _mail;
            set
            {
                if (_mail != value)
                {
                    _mail = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isLoading;

        public bool IsLoading
        {
            get => _isLoading;
            set {
                if (_isLoading != value)
                {
                    _isLoading = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(MainContentVisibility));
                }
            }
        }

        public Visibility MainContentVisibility => IsLoading ? Visibility.Collapsed : Visibility.Visible;

        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task<bool> LoadDoctorInformationAsync(int userId)
        {
            System.Diagnostics.Debug.WriteLine("LoadDoctorInformationAsync called");
            try
            {
                IsLoading = true;
                System.Diagnostics.Debug.WriteLine("IsLoading set to true");

                // Fetch doctor info
                var doctor = await _doctorProxy.GetDoctorByUserIdAsync(userId);
                if (doctor == null)
                {
                    System.Diagnostics.Debug.WriteLine("Doctor not found");
                    return false;
                }

                // Fetch user info
                var user = await _userProxy.GetUserByIdAsync(userId);
                if (user == null)
                {
                    System.Diagnostics.Debug.WriteLine("User not found");
                    return false;
                }

                // Fetch department info
                //var department = await _doctorProxy.GetDepartmentByIdAsync(doctor.DepartmentId);

                DoctorName = user.Name;
                DepartmentId = doctor.DepartmentId;
                DepartmentName = DefaultDepartmentName; // department?.Name ?? 
                Rating = doctor.DoctorRating;
                CareerInfo = user.Role ?? DefaultCareerInfo; // Or another appropriate field
                AvatarUrl = ""; // Set if available
                PhoneNumber = user.PhoneNumber;
                Mail = user.Mail;

                System.Diagnostics.Debug.WriteLine("Doctor, user, and department info loaded");
                return true;
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine($"Exception: {exception.Message}");
                Debug.WriteLine($"Error in ViewModel: {exception.Message}");

                // Set error state
                DoctorName = DefaultErrorDoctorName;
                DepartmentName = DefaultErrorDepartmentName;
                CareerInfo = DefaultErrorCareerInfo;

                return false;
            }
            finally
            {
                IsLoading = false;
                System.Diagnostics.Debug.WriteLine("IsLoading set to false (finally)");
            }
        }

        public async Task<bool> UpdateDoctorNameAsync(string newName)
        {
            return await UpdateFieldAsync(() => _doctorService.UpdateDoctorName(UserId, newName), 
                () => DoctorName = newName,
                () => OriginalDoctor.Clone(doctorName: newName)
            );
        }

        public async Task<bool> UpdateDepartmentAsync(int newDepartmentId)
        {
            return await UpdateFieldAsync(() => _doctorService.UpdateDepartment(UserId, newDepartmentId), 
                () => DepartmentId = newDepartmentId, 
                () => OriginalDoctor.Clone(departmentId: newDepartmentId)
            );
        }

        public async Task<bool> UpdateCareerInfoAsync(string newCareerInformation)
        {
            return await UpdateFieldAsync(() => _doctorService.UpdateCareerInfo(UserId, newCareerInformation), 
                () => CareerInfo = newCareerInformation, 
                () => OriginalDoctor.Clone(careerInformation: newCareerInformation) 
            );
        }

        public async Task<bool> UpdateAvatarUrlAsync(string newAvatarUrl)
        {
            return await UpdateFieldAsync(() => _doctorService.UpdateAvatarUrl(UserId, newAvatarUrl), 
                () => AvatarUrl = newAvatarUrl, 
                () => OriginalDoctor.Clone(avatarUrl: newAvatarUrl)
            );
        }

        public async Task<bool> UpdatePhoneNumberAsync(string newPhoneNumber)
        {
            return await UpdateFieldAsync(() => _doctorService.UpdatePhoneNumber(UserId, newPhoneNumber),
                () => PhoneNumber = newPhoneNumber, 
                () => OriginalDoctor.Clone(phoneNumber: newPhoneNumber) 
            );
        }


        public async Task<bool> UpdateMailAsync(string newEmail)
        {
            return await UpdateFieldAsync(
                () => _doctorService.UpdateEmail(UserId, newEmail), 
                () => Mail = newEmail, 
                () => OriginalDoctor.Clone(email: newEmail) 
            );
        }


        public void RevertChanges()
        {
            DoctorName = OriginalDoctor.DoctorName;
            DepartmentName = OriginalDoctor.DepartmentName;
            CareerInfo = OriginalDoctor.CareerInfo;
            AvatarUrl = OriginalDoctor.AvatarUrl;
            PhoneNumber = OriginalDoctor.PhoneNumber;
            Mail = OriginalDoctor.Mail;
        }

        public async Task<(bool updateSuccessful, string? errorMessage)> TryUpdateDoctorProfileAsync()
        {
            try
            {
                if (OriginalDoctor == null)
                    return (false, "Original doctor data is not initialized.");

                bool changeMade = false;

                if (DoctorName != OriginalDoctor.DoctorName)
                    changeMade |= await UpdateDoctorNameAsync(DoctorName);

                if (DepartmentName != OriginalDoctor.DepartmentName)
                    changeMade |= await UpdateDepartmentAsync(DepartmentId);

                if (CareerInfo != OriginalDoctor.CareerInfo)
                    changeMade |= await UpdateCareerInfoAsync(CareerInfo);

                if (AvatarUrl != OriginalDoctor.AvatarUrl)
                    changeMade |= await UpdateAvatarUrlAsync(AvatarUrl);

                if (PhoneNumber != OriginalDoctor.PhoneNumber)
                    changeMade |= await UpdatePhoneNumberAsync(PhoneNumber);

                if (Mail != OriginalDoctor.Mail)
                    changeMade |= await UpdateMailAsync(Mail);


                return (changeMade, null);
            }
            catch (Exception ex)
            {
                RevertChanges();
                return (false, ex.Message);
            }
        }

        private async Task<bool> UpdateFieldAsync(Func<Task<bool>> updateServiceFunction, Action updatePropertyFunction, Func<DoctorModel> updateModelFunction)
        {
            try
            {
                IsLoading = true;
                bool result = await updateServiceFunction();
                if (result)
                {
                    updatePropertyFunction(); 
                    OriginalDoctor = updateModelFunction();
                }
                return result;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}