using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary.IRepository;
using Data;
using ClassLibrary.Domain;
using Entity;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Repository
{
    /// <summary>
    /// Repository class for managing doctor-related database operations.
    /// </summary>
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ApplicationDbContext _db_context;

        public DoctorRepository(ApplicationDbContext db_context)
        {
            this._db_context = db_context;
        }

        /// <inheritdoc/>
        public async Task<List<Doctor>> getAllDoctorsAsync()
        {
            List<DoctorEntity> doctorEntities = await dbContext.Doctors
                .Include(d => d.Department)
                .Include(d => d.User)
                .ToListAsync();

            List<Doctor> doctors = doctor_entities.Select(d => new Doctor
            {
                UserId = d.UserId,
                DepartmentId = d.DepartmentId,
                DoctorRating = d.DoctorRating,
                LicenseNumber = d.LicenseNumber,
                Name = d.User.Name
            }).ToList();

            return doctors;
        }

        /// <inheritdoc/>
        public async Task<Doctor> getDoctorByUserIdAsync(int id)
        {
            var doctorEntity = await dbContext.Doctors
                .Include(d => d.Department)
                .Include(d => d.User)
                .FirstOrDefaultAsync(d => d.UserId == id);

            if (doctor_entity == null)
            {
                throw new Exception($"Doctor with user ID {id} not found.");
            }

            return new Doctor
            {
                UserId = doctorEntity.UserId,
                DepartmentId = doctorEntity.DepartmentId,
                DoctorRating = doctorEntity.DoctorRating,
                LicenseNumber = doctorEntity.LicenseNumber,
                Name = doctorEntity.User.Name
            };
        }

        /// <inheritdoc/>
        public async Task<List<Doctor>> getDoctorsByDepartmentIdAsync(int department_id)
        {
            var doctorEntities = await dbContext.Doctors
                .Include(d => d.Department)
                .Include(d => d.User)
                .Where(d => d.DepartmentId == departmentId)
                .ToListAsync();

            return doctor_entities.Select(d => new Doctor
            {
                UserId = d.UserId,
                DepartmentId = d.DepartmentId,
                DoctorRating = d.DoctorRating,
                LicenseNumber = d.LicenseNumber,
                Name = d.User.Name
            }).ToList();
        }

        /// <inheritdoc/>
        public async Task addDoctorAsync(Doctor doctor)
        {
            var existing_patient = await _db_context.Patients.FindAsync(doctor.userId);
            if (existing_patient != null)
            {
                throw new InvalidOperationException("User is already registered as a patient and cannot be a doctor.");
            }

            var doctor_entity = new DoctorEntity
            {
                userId = doctor.userId,
                departmentId = doctor.departmentId,
                doctorRating = doctor.doctorRating,
                licenseNumber = doctor.licenseNumber
            };

            _db_context.Doctors.Add(doctor_entity);
            await _db_context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task deleteDoctorAsync(int id)
        {
            var doctor_entity = await _db_context.Doctors.FindAsync(id);
            if (doctor_entity == null)
            {
                throw new Exception($"Doctor with user ID {id} not found.");
            }

            _db_context.Doctors.Remove(doctor_entity);
            await _db_context.SaveChangesAsync();
        }
    }
}