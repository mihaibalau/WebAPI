using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.IRepository
{
    public interface ILogRepository
    {
        /// <summary>
        /// Gets all logs.
        /// </summary>
        /// <returns></returns>
        Task<List<Log>> GetAllLogsAsync();

        /// <summary>
        /// Gets a log by its unique identifier.
        /// </summary>
        /// <param name="id">the id of the log.</param>
        /// <returns>The log with the given id.</returns>
        Task<Log> GetLogByIdAsync(int id);

        /// <summary>
        /// Gets a log by its user id.
        /// </summary>
        /// <param name="userId">The id of the user.</param>
        /// <returns>The log with the given user id.</returns>
        Task<Log> GetLogByUserIdAsync(int userId);

        /// <summary>
        /// Adds a new log to the system.
        /// </summary>
        /// <param name="log">The log to be added.</param>
        /// <returns> task representing the asynchronous operation.</returns>
        Task AddLogAsync(Log log);

        /// <summary>
        /// Deletes a log by its unique identifier.
        /// </summary>
        /// <param name="id">The id of the log</param>
        /// <returns> task representing the asynchronous operation.</returns>
        Task DeleteLogAsync(int id);
    }
}
