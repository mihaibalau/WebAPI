using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClassLibrary.IRepository;
using WinUI.Model;
using WinUI.Proxy;

namespace WinUI.Service
{
    public class DoctorService : IDoctorService
    {
        private readonly DoctorsProxy _doctorRepository;
        private readonly IUserRepository _userRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public DoctorModel DoctorInformation { get; private set; } = DoctorModel.Default;

        public DoctorService(IDoctorRepository doctorRepository, IUserRepository userRepository, IDepartmentRepository departmentRepository)
        {
            _doctorRepository = doctorRepository ?? throw new ArgumentNullException(nameof(doctorRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _departmentRepository = departmentRepository ?? throw new ArgumentNullException(nameof(departmentRepository));
        }

        public async Task<bool> LoadDoctorInformationByUserId(int userId)
        {
            try
            {
                var doctor = await _doctorRepository.GetDoctorByUserIdAsync(userId);
                if (doctor == null)
                    return false;

                var user = await _userRepository.GetUserByIdAsync(userId);
                var department = await _departmentRepository.GetDepartmentByIdAsync(doctor.DepartmentId);
                if (user == null || department == null)
                    return false;

                DoctorInformation = new DoctorModel
                {
                    DoctorId = user.UserId,
                    DoctorName = user.Name,
                    DepartmentId = department.Id,
                    DepartmentName = department.Name,
                    Rating = doctor.DoctorRating,
                    PhoneNumber = user.PhoneNumber,
                    Mail = user.Mail,
                    CareerInfo = user.Role, // Placeholder if CareerInfo is not defined elsewhere
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
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) return false;

            user.Name = newName;
            await _userRepository.DeleteUserAsync(userId);
            await _userRepository.AddUserAsync(user);
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
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) return false;

            user.Role = careerInfo;
            await _userRepository.DeleteUserAsync(userId);
            await _userRepository.AddUserAsync(user);
            return true;
        }

        public async Task<bool> UpdateAvatarUrl(int userId, string avatarUrl)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) return false;

            user.Address = avatarUrl; 
            await _userRepository.DeleteUserAsync(userId);
            await _userRepository.AddUserAsync(user);
            return true;
        }

        public async Task<bool> UpdatePhoneNumber(int userId, string phoneNumber)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) return false;

            user.PhoneNumber = phoneNumber;
            await _userRepository.DeleteUserAsync(userId);
            await _userRepository.AddUserAsync(user);
            return true;
        }

        public async Task<bool> UpdateEmail(int userId, string email)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) return false;

            user.Mail = email;
            await _userRepository.DeleteUserAsync(userId);
            await _userRepository.AddUserAsync(user);
            return true;
        }
        
    }
}
