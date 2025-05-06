using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClassLibrary.IRepository;
using WinUI.Model;
using WinUI.Proxy;
using WinUI.Model;
using WinUI.Repository;

namespace WinUI.Service
{
    public class DoctorService : IDoctorService
    {
        private readonly DoctorsProxy _doctorRepository;
        private readonly LogInProxy _userRepository;

        public DoctorModel DoctorInformation { get; private set; } = DoctorModel.Default;

        public DoctorService(IDoctorRepository doctorRepository, ILogInRepository userRepository)
        {
            _doctorRepository = (DoctorsProxy?)(doctorRepository ?? throw new ArgumentNullException(nameof(doctorRepository)));
            _userRepository = (LogInProxy?)(userRepository ?? throw new ArgumentNullException(nameof(userRepository)));
        }

        public async Task<bool> LoadDoctorInformationByUserId(int userId)
        {
            try
            {
                var doctor = await _doctorRepository.GetDoctorByUserIdAsync(userId);
                if (doctor == null)
                    return false;

                var user = await _userRepository.getUserById(userId);
                var department = await _doctorRepository.GetDepartmentByIdAsync(doctor.DepartmentId);
                if (user == null || department == null)
                    return false;

                DoctorInformation = new DoctorModel
                {
                    DoctorId = user.user_id,
                    DoctorName = user.username,
                    DepartmentId = department.Id,
                    DepartmentName = department.Name,
                    Rating = doctor.DoctorRating,
                    Mail = user.mail,
                    CareerInfo = user.role, // Placeholder if CareerInfo is not defined elsewhere
                    AvatarUrl = "" // Placeholder; update if this exists in your model
                };

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading doctor info: {ex.Message}");
                return false;
            }
        }
        
public async Task<bool> UpdateDoctorName(int userId, string newName)
        {
            var user = await _userRepository.getUserById(userId);
            if (user == null) return false;

            // Create a new instance of UserAuthModel with updated username since the property is read-only  
            var updatedUser = new UserAuthModel(user.user_id, newName, user.password, user.mail, user.role);

            //await _userRepository.createAccount(User);
            //await _userRepository.DeleteUserAsync(userId);
            //await _userRepository.AddUserAsync(updatedUser);
            return true;
        }

        public async Task<bool> UpdateDepartment(int userId, int departmentId)
        {
            var doctor = await _doctorRepository.GetDoctorByUserIdAsync(userId);
            if (doctor == null) return false;

            doctor.DepartmentId = departmentId;
            await _doctorRepository.DeleteDoctorAsync(userId);
            await _doctorRepository.AddDoctorAsync(doctor);
            return true;
        }

        public async Task<bool> UpdateCareerInfo(int userId, string careerInfo)
        {
            var user = await _userRepository.getUserById(userId);
            if (user == null) return false;

            var updatedUser = new UserAuthModel(user.user_id, careerInfo, user.password, user.mail, user.role);
            //await _userRepository.DeleteUserAsync(userId);
            //await _userRepository.AddUserAsync(user);
            return true;
        }

        public async Task<bool> UpdateAvatarUrl(int userId, string avatarUrl)
        {
            var user = await _userRepository.getUserById(userId);
            if (user == null) return false;

            //user.Address = avatarUrl; 
            //await _userRepository.DeleteUserAsync(userId);
            //await _userRepository.AddUserAsync(user);
            return true;
        }

        public async Task<bool> UpdatePhoneNumber(int userId, string phoneNumber)
        {
            var user = await _userRepository.getUserById(userId);
            if (user == null) return false;

            //user.PhoneNumber = phoneNumber;
            //await _userRepository.DeleteUserAsync(userId);
            //await _userRepository.AddUserAsync(user);
            return true;
        }

        public async Task<bool> UpdateEmail(int userId, string email)
        {
            var user = await _userRepository.getUserById(userId);
            if (user == null) return false;

            //user.Mail = email;
            //await _userRepository.DeleteUserAsync(userId);
            //await _userRepository.AddUserAsync(user);
            return true;
        }
        
    }
}
