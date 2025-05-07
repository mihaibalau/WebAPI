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
        public async Task<List<Patient>> getAllPatientsAsync()
        {
            var patientEntities = await dbContext.Patients.ToListAsync();

            return patientEntities.Select(p => new Patient
            {
                userId = p.userId,
                bloodType = p.bloodType,
                EmergencyContact = p.emergencyContact,
                allergies = p.allergies,
                weight = p.weight,
                height = p.height
            }).ToList();
        }

        /// <inheritdoc/>
        public async Task<Patient> getPatientByUserIdAsync(int id)
        {
            var patientEntity = await dbContext.Patients.FindAsync(id);

            if (patientEntity == null)
            {
                throw new Exception($"Patient with User ID {id} not found.");
            }

            return new Patient
            {
                userId = patientEntity.userId,
                bloodType = patientEntity.bloodType,
                EmergencyContact = patientEntity.emergencyContact,
                allergies = patientEntity.allergies,
                weight = patientEntity.weight,
                height = patientEntity.height
            };
        }

        /// <inheritdoc/>
        public async Task addPatientAsync(Patient patient)
        {
            var existingDoctor = await dbContext.Doctors.FindAsync(patient.userId);
            if (existingDoctor != null)
            {
                throw new InvalidOperationException("User is already registered as a doctor and cannot be a patient.");
            }

            var patientEntity = new PatientEntity
            {
                userId = patient.userId,
                bloodType = patient.bloodType,
                emergencyContact = patient.EmergencyContact,
                allergies = patient.allergies,
                weight = patient.weight,
                height = patient.height
            };

            dbContext.Patients.Add(patientEntity);
            await dbContext.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task deletePatientAsync(int id)
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
