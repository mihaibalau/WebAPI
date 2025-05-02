using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary.IRepository;
using Domain;
using WinUI.Model;

namespace WinUI.Service
{
    public class SearchDoctorsService : ISearchDoctorsService
    {
        private readonly IDoctorRepository _doctorsRepository;
        public List<DoctorModel> AvailableDoctors { get; private set; }

        public SearchDoctorsService(IDoctorRepository doctorsDatabaseHelper)
        {
            _doctorsRepository = doctorsDatabaseHelper;
            AvailableDoctors = new List<DoctorModel>();
        }

        public async Task LoadDoctors(string searchTerm)
        {
            try
            {
                AvailableDoctors.Clear();
                var doctorsByDepartment = GetDoctorsByDepartmentPartialName(searchTerm);
                var doctorsByName = GetDoctorsByPartialDoctorName(searchTerm);

                foreach (var doctor in doctorsByDepartment)
                {
                    AvailableDoctors.Add(doctor);
                }

                foreach (var doctor in doctorsByName)
                {
                    var isAlreadyAdded = AvailableDoctors.Any(existingDoctor => existingDoctor.DoctorId == doctor.DoctorId);
                    if (!isAlreadyAdded)
                    {
                        AvailableDoctors.Add(doctor);
                    }
                }

                AvailableDoctors = SortDoctorsByDefaultCriteria(AvailableDoctors);
            }
            catch (Exception error)
            {
                Console.WriteLine($"Error loading doctors: {error.Message}");
            }
        }

        public List<DoctorModel> GetDoctorsByDepartmentPartialName(string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    return new List<DoctorModel>();
                }

                var allDoctors = _doctorsRepository.GetAllDoctorsAsync().Result;
                if (allDoctors == null)
                {
                    return new List<DoctorModel>();
                }

                // Filter doctors whose department name contains the search term (case-insensitive)
                var filteredDoctors = allDoctors
                    .Where(doctor => doctor.Department?.Name?.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
                    .Select(doctor => new DoctorModel
                    {
                        DoctorId = doctor.DoctorId,
                        DoctorName = doctor.Name ?? DoctorModel.DefaultName,
                        DepartmentId = doctor.Department?.DepartmentId ?? DoctorModel.DefaultDepartmentId,
                        DepartmentName = doctor.Department?.Name ?? DoctorModel.DefaultDepartmentName,
                        Rating = doctor.Rating ?? DoctorModel.DefaultRating,
                        CareerInfo = doctor.CareerInfo ?? DoctorModel.DefaultCareerInfo,
                        AvatarUrl = doctor.AvatarUrl,
                        PhoneNumber = doctor.PhoneNumber ?? DoctorModel.DefaultPhone,
                        Mail = doctor.Email ?? DoctorModel.DefaultEmail
                    })
                    .ToList();

                return filteredDoctors;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching doctors by department: {ex.Message}");
                return new List<DoctorModel>();
            }
        }

        public List<DoctorModel> GetDoctorsByPartialDoctorName(string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    return new List<DoctorModel>();
                }

                var allDoctors = _doctorsRepository.GetAllDoctorsAsync().Result;
                if (allDoctors == null)
                {
                    return new List<DoctorModel>();
                }

                // Filter doctors whose name contains the search term (case-insensitive)
                var filteredDoctors = allDoctors
                    .Where(doctor =>
                        doctor.Name?.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0
                    )
                    .Select(doctor => new DoctorModel
                    {
                        DoctorId = doctor.DoctorId,
                        DoctorName = doctor.Name ?? DoctorModel.DefaultName,
                        DepartmentId = doctor.Department?.DepartmentId ?? DoctorModel.DefaultDepartmentId,
                        DepartmentName = doctor.Department?.Name ?? DoctorModel.DefaultDepartmentName,
                        Rating = doctor.Rating ?? DoctorModel.DefaultRating,
                        CareerInfo = doctor.CareerInfo ?? DoctorModel.DefaultCareerInfo,
                        AvatarUrl = doctor.AvatarUrl,
                        PhoneNumber = doctor.PhoneNumber ?? DoctorModel.DefaultPhone,
                        Mail = doctor.Email ?? DoctorModel.DefaultEmail
                    })
                    .ToList();

                return filteredDoctors;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching doctors by name: {ex.Message}");
                return new List<DoctorModel>();
            }
        }

        public List<DoctorModel> GetSearchedDoctors()
        {
            return AvailableDoctors;
        }

        public List<DoctorModel> GetDoctorsSortedBy(SortCriteria sortCriteria)
        {
            switch (sortCriteria)
            {
                case SortCriteria.RatingHighToLow:
                    return AvailableDoctors.OrderByDescending(doctor => doctor.Rating).ToList();
                case SortCriteria.RatingLowToHigh:
                    return AvailableDoctors.OrderBy(doctor => doctor.Rating).ToList();
                case SortCriteria.NameAscending:
                    return AvailableDoctors.OrderBy(doctor => doctor.DoctorName).ToList();
                case SortCriteria.NameDescending:
                    return AvailableDoctors.OrderByDescending(doctor => doctor.DoctorName).ToList();
                case SortCriteria.DepartmentAscending:
                    return AvailableDoctors.OrderBy(doctor => doctor.DepartmentName).ToList();
                case SortCriteria.RatingThenNameThenDepartment:
                    return SortDoctorsByDefaultCriteria(AvailableDoctors);
                default:
                    return AvailableDoctors;
            }
        }

        private List<DoctorModel> SortDoctorsByDefaultCriteria(List<DoctorModel> doctors)
        {
            return doctors
                .OrderByDescending(doctor => doctor.Rating)
                .ThenBy(doctor => doctor.DoctorName)
                .ThenBy(doctor => doctor.DepartmentName)
                .ToList();
        }
    }

    public enum SortCriteria
    {
        RatingHighToLow,
        RatingLowToHigh,
        NameAscending,
        NameDescending,
        DepartmentAscending,
        RatingThenNameThenDepartment
    }
}