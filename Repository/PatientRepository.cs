using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary.IRepository;
using Data;
using Domain;
using Entity;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Repository
{
    /// <summary>
    /// Repository class for managing patient-related database operations.
    /// </summary>
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext dbContext;

        public PatientRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<List<Patient>> GetAllPatientsAsync()
        {
            var patientEntities = await dbContext.Patients.ToListAsync();

            return patientEntities.Select(p => new Patient
            {
                UserId = p.UserId,
                BloodType = p.BloodType,
                EmergencyContact = p.EmergencyContact,
                Allergies = p.Allergies,
                Weight = p.Weight,
                Height = p.Height
            }).ToList();
        }

        /// <inheritdoc/>
        public async Task<Patient> GetPatientByUserIdAsync(int id)
        {
            var patientEntity = await dbContext.Patients.FindAsync(id);

            if (patientEntity == null)
            {
                throw new Exception($"Patient with User ID {id} not found.");
            }

            return new Patient
            {
                UserId = patientEntity.UserId,
                BloodType = patientEntity.BloodType,
                EmergencyContact = patientEntity.EmergencyContact,
                Allergies = patientEntity.Allergies,
                Weight = patientEntity.Weight,
                Height = patientEntity.Height
            };
        }

        /// <inheritdoc/>
        public async Task AddPatientAsync(Patient patient)
        {
            var existingDoctor = await dbContext.Doctors.FindAsync(patient.UserId);
            if (existingDoctor != null)
            {
                throw new InvalidOperationException("User is already registered as a doctor and cannot be a patient.");
            }

            var patientEntity = new PatientEntity
            {
                UserId = patient.UserId,
                BloodType = patient.BloodType,
                EmergencyContact = patient.EmergencyContact,
                Allergies = patient.Allergies,
                Weight = patient.Weight,
                Height = patient.Height
            };

            dbContext.Patients.Add(patientEntity);
            await dbContext.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task DeletePatientAsync(int id)
        {
            var patientEntity = await dbContext.Patients.FindAsync(id);
            if (patientEntity == null)
            {
                throw new Exception($"Patient with User ID {id} not found.");
            }

            dbContext.Patients.Remove(patientEntity);
            await dbContext.SaveChangesAsync();
        }
    }
}
