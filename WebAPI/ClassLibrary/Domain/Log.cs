namespace Domain
{
    /// <summary>
    /// Logs class.
    /// </summary>
    public class Log
    {
        /// <summary>
        /// Gets or sets the unique identifier for the log entry.
        /// </summary>
        public int LogId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the user associated with the log entry.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the action type fot the log entry.
        /// </summary>
        public string ActionType{ get; set; }

        /// <summary>
        /// Gets or sets the timestemp of the log entry.
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}
