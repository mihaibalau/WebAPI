namespace Domain
{
    /// <summary>
    /// Represents a patient in the system.
    /// </summary>
    public class Patient
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the blood type of the patient.
        /// </summary>
        public string BloodType { get; set; }

        /// <summary>
        /// Gets or sets the emergency contact information of the patient.
        /// </summary>
        public string EmergencyContact { get; set; }

        /// <summary>
        /// Gets or sets the allergies of the patient.
        /// </summary>
        public string Allergies { get; set; }

        /// <summary>
        /// Gets or sets the weight of the patient
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// Gets or sets the height of the patient
        /// </summary>
        public int Height { get; set; }
    }
}
