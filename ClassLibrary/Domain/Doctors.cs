namespace Domain
{
    /// <summary>
    /// Represents a doctor in the system.
    /// </summary>
    class Doctors
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the departement identifier for the doctor.
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// Gets or sets doctor rating for the doctor.
        /// </summary>
        public float DoctorRating { get; set; }

        /// <summary>
        /// Gets or sets the licence number for the doctor.
        /// </summary>
        public string LicenceNumber { get; set; }
    }
}
