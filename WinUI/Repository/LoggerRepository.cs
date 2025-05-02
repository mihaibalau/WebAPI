// <copyright file="LoggerRepository.cs"  company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using WinUI.Model;

namespace WinUI.Repository
{
    /// <summary>
    /// Service for handling database operations related to system logs.
    /// </summary>
    public class LoggerRepository : ILoggerRepository
    {
        private readonly HttpClient client;
        private readonly string baseApiUrl;
        private readonly JsonSerializerOptions jsonOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerRepository"/> class.
        /// </summary>
        /// <param name="configProvider">The configuration provider for database connection information.</param>
        /// <exception cref="ArgumentNullException">Thrown when configProvider is null.</exception>
        public LoggerRepository()
        {
            client = new HttpClient();
            baseApiUrl = "https://localhost:7004/";

            jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        /// <summary>
        /// Gets all logs from the database.
        /// </summary>
        /// <returns>A task representing the asynchronous operation with a list of log entries.</returns>
        public async Task<List<LogEntryModel>> GetAllLogs()
        {
            try
            {
                // The ApiLogDto is a temporary class to handle the API response
                var response = await client.GetAsync($"{baseApiUrl}/api/log");
                response.EnsureSuccessStatusCode();

                var apiLogs = await response.Content.ReadFromJsonAsync<List<ApiLogDto>>(jsonOptions);
                return ConvertApiLogsToLogEntryModels(apiLogs);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new List<LogEntryModel>();
            }
        }

        /// <summary>
        /// Gets logs for a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user whose logs to retrieve.</param>
        /// <returns>A task representing the asynchronous operation with a list of log entries.</returns>
        public async Task<List<LogEntryModel>> GetLogsByUserId(int userId)
        {
            try
            {
                var response = await client.GetAsync($"{baseApiUrl}/api/log");
                response.EnsureSuccessStatusCode();
                var apiLogs = await response.Content.ReadFromJsonAsync<List<ApiLogDto>>(jsonOptions);
                var filteredApiLogs = apiLogs.FindAll(log => log.UserId == userId);
                return ConvertApiLogsToLogEntryModels(filteredApiLogs);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new List<LogEntryModel>();
            }
        }
        

        /// <summary>
        /// Gets logs from before a specific timestamp.
        /// </summary>
        /// <param name="beforeTimeStamp">The timestamp to filter by.</param>
        /// <returns>A task representing the asynchronous operation with a list of log entries.</returns>
        public async Task<List<LogEntryModel>> GetLogsBeforeTimestamp(DateTime beforeTimeStamp)
        {
            try
            {
                var response = await client.GetAsync($"{baseApiUrl}/api/log");
                response.EnsureSuccessStatusCode();
                var apiLogs = await response.Content.ReadFromJsonAsync<List<ApiLogDto>>(jsonOptions);
                var filteredApiLogs = apiLogs.FindAll(log => log.Timestamp < beforeTimeStamp);
                return ConvertApiLogsToLogEntryModels(filteredApiLogs);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new List<LogEntryModel>();
            }
        }

        /// <summary>
        /// Records a new action in the log.
        /// </summary>
        /// <param name="userId">The ID of the user who performed the action.</param>
        /// <param name="actionType">The type of action performed.</param>
        /// <returns>A task representing the asynchronous operation with a boolean indicating success.</returns>
        public async Task<bool> LogAction(int userId, ActionType actionType)
        {
            try
            {
                var logData = new ApiLogDto
                {
                    UserId = userId,
                    ActionType = actionType.ToString(),
                    Timestamp = DateTime.UtcNow
                };

                var response = await client.PostAsJsonAsync($"{baseApiUrl}/api/log", logData);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Gets logs by action type.
        /// </summary>
        /// <param name="actionType">The action type to filter by.</param>
        /// <returns>A task representing the asynchronous operation with a list of log entries.</returns>
        public async Task<List<LogEntryModel>> GetLogsByActionType(ActionType actionType)
        {
            try
            {
                var response = await client.GetAsync($"{baseApiUrl}/api/log");
                response.EnsureSuccessStatusCode();
                var apiLogs = await response.Content.ReadFromJsonAsync<List<ApiLogDto>>(jsonOptions);
                var filteredApiLogs = apiLogs.FindAll(log => log.ActionType == actionType.ToString());
                return ConvertApiLogsToLogEntryModels(filteredApiLogs);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new List<LogEntryModel>();
            }
        }

        /// <summary>
        /// Gets logs matching specific parameters without filtering by user ID.
        /// </summary>
        /// <param name="actionType">The action type to filter by.</param>
        /// <param name="beforeTimeStamp">The timestamp to filter by.</param>
        /// <returns>A task representing the asynchronous operation with a list of log entries.</returns>
        public async Task<List<LogEntryModel>> GetLogsWithParametersWithoutUserId(ActionType actionType, DateTime beforeTimeStamp)
        {
            try
            {
                var response = await client.GetAsync($"{baseApiUrl}/api/log");
                response.EnsureSuccessStatusCode();
                var apiLogs = await response.Content.ReadFromJsonAsync<List<ApiLogDto>>(jsonOptions);
                var filteredApiLogs = apiLogs.FindAll(log => 
                    log.ActionType == actionType.ToString() &&
                    log.Timestamp < beforeTimeStamp);
                return ConvertApiLogsToLogEntryModels(filteredApiLogs);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new List<LogEntryModel>();
            }
        }

        /// <summary>
        /// Gets logs matching specific parameters.
        /// </summary>
        /// <param name="userId">The user ID to filter by.</param>
        /// <param name="actionType">The action type to filter by.</param>
        /// <param name="beforeTimeStamp">The timestamp to filter by.</param>
        /// <returns>A task representing the asynchronous operation with a list of log entries.</returns>
        public async Task<List<LogEntryModel>> GetLogsWithParameters(int userId, ActionType actionType, DateTime beforeTimeStamp)
        {
            try
            {
                var response = await client.GetAsync($"{baseApiUrl}/api/log");
                response.EnsureSuccessStatusCode();
                var apiLogs = await response.Content.ReadFromJsonAsync<List<ApiLogDto>>(jsonOptions);
                var filteredApiLogs = apiLogs.FindAll(log =>
                    log.UserId == userId &&
                    log.ActionType == actionType.ToString() &&
                    log.Timestamp < beforeTimeStamp);
                return ConvertApiLogsToLogEntryModels(filteredApiLogs);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new List<LogEntryModel>();
            }
        }

        /// <summary>
        /// Converts API log models to internal log entry models.
        /// </summary>
        private List<LogEntryModel> ConvertApiLogsToLogEntryModels(List<ApiLogDto> apiLogs)
        {
            if (apiLogs == null)
                return new List<LogEntryModel>();

            var result = new List<LogEntryModel>();

            foreach (var apiLog in apiLogs)
            {
                if (Enum.TryParse<ActionType>(apiLog.ActionType, out var actionType))
                {
                    result.Add(new LogEntryModel(
                        apiLog.LogId,
                        apiLog.UserId,
                        actionType,
                        apiLog.Timestamp
                    ));
                }
            }

            return result;
        }
    }

    public class ApiLogDto
    {
        public int LogId { get; set; }
        public int UserId { get; set; }
        public string ActionType { get; set; }
        public DateTime Timestamp { get; set; }
    }
}

