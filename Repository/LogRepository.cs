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
    /// Repository class for managing log-related database operations.
    /// </summary>
    public class LogRepository : ILogRepository
    {
        private readonly ApplicationDbContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public LogRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<List<Log>> GetAllLogsAsync()
        {
            List<LogEntity> logEntities = await dbContext.Logs.ToListAsync();

            return logEntities.Select(log => new Log
            {
                LogId = log.LogId,
                UserId = (int)log.UserId,
                ActionType = log.ActionType,
                Timestamp = log.Timestamp
            }).ToList();
        }

        /// <inheritdoc/>
        public async Task<Log> GetLogByIdAsync(int id)
        {
            var logEntity = await dbContext.Logs.FindAsync(id);
            if (logEntity == null)
            {
                throw new Exception($"Log with ID {id} not found.");
            }

            return new Log
            {
                LogId = logEntity.LogId,
                UserId = (int)logEntity.UserId,
                ActionType = logEntity.ActionType,
                Timestamp = logEntity.Timestamp
            };
        }

        /// <inheritdoc/>
        public async Task<Log> GetLogByUserIdAsync(int userId)
        {
            var logEntity = await dbContext.Logs
                .FirstOrDefaultAsync(log => log.UserId == userId);

            if (logEntity == null)
            {
                throw new Exception($"Log for user ID {userId} not found.");
            }

            return new Log
            {
                LogId = logEntity.LogId,
                UserId = (int)logEntity.UserId,
                ActionType = logEntity.ActionType,
                Timestamp = logEntity.Timestamp
            };
        }

        /// <inheritdoc/>
        public async Task AddLogAsync(Log log)
        {
            var logEntity = new LogEntity
            {
                UserId = log.UserId,
                ActionType = log.ActionType,
                Timestamp = log.Timestamp
            };

            dbContext.Logs.Add(logEntity);
            await dbContext.SaveChangesAsync();

            log.LogId = logEntity.LogId;
        }

        /// <inheritdoc/>
        public async Task DeleteLogAsync(int id)
        {
            var logEntity = await dbContext.Logs.FindAsync(id);
            if (logEntity == null)
            {
                throw new Exception($"Log with ID {id} not found.");
            }

            dbContext.Logs.Remove(logEntity);
            await dbContext.SaveChangesAsync();
        }
    }
}
