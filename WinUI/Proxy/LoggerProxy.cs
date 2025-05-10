// <copyright file="LoggerProxy.cs"  company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WinUI.Model;
using WinUI.Repository;

namespace WinUI.Proxy
{
    /// <summary>
    /// Service for handling database operations related to system logs.
    /// </summary>
    public class LoggerProxy : ILoggerRepository
    {
        private readonly HttpClient _httpClient;
        private static readonly string _baseApiUrl = Config._base_api_url;
        private readonly JsonSerializerOptions _jsonOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerProxy"/> class.
        /// </summary>
        public LoggerProxy()
        {
            this._httpClient = new HttpClient();

            this._jsonOptions = new JsonSerializerOptions
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
                HttpResponseMessage _response = await this._httpClient.GetAsync(_baseApiUrl + "api/log");
                _response.EnsureSuccessStatusCode();

                List<ApiLogDto> _apiLogs = await _response.Content.ReadFromJsonAsync<List<ApiLogDto>>(_jsonOptions);
                return this.ConvertApiLogsToLogEntryModels(_apiLogs);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Exception: {exception.Message}");
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
                HttpResponseMessage _response = await this._httpClient.GetAsync(_baseApiUrl + "api/log");
                _response.EnsureSuccessStatusCode();
                List<ApiLogDto> _apiLogs = await _response.Content.ReadFromJsonAsync<List<ApiLogDto>>(_jsonOptions);
                List<ApiLogDto> _filteredApiLogs = _apiLogs.FindAll(log => log._user_id == userId);
                return this.ConvertApiLogsToLogEntryModels(_filteredApiLogs);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Exception: {exception.Message}");
                return new List<LogEntryModel>();
            }
        }

        /// <summary>
        /// Gets logs from before a specific timestamp.
        /// </summary>
        /// <param name="beforeTimestamp">The timestamp to filter by.</param>
        /// <returns>A task representing the asynchronous operation with a list of log entries.</returns>
        public async Task<List<LogEntryModel>> GetLogsBeforeTimestamp(DateTime beforeTimestamp)
        {
            try
            {
                HttpResponseMessage _response = await this._httpClient.GetAsync(_baseApiUrl + "api/log");
                _response.EnsureSuccessStatusCode();
                List<ApiLogDto> _apiLogs = await _response.Content.ReadFromJsonAsync<List<ApiLogDto>>(_jsonOptions);
                List<ApiLogDto> _filteredApiLogs = _apiLogs.FindAll(log => log._timestamp < beforeTimestamp);
                return this.ConvertApiLogsToLogEntryModels(_filteredApiLogs);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Exception: {exception.Message}");
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
                ApiLogDto _logData = new ApiLogDto
                {
                    _user_id = userId,
                    _action_type = actionType.ToString(),
                    _timestamp = DateTime.UtcNow
                };

                HttpResponseMessage _response = await this._httpClient.PostAsJsonAsync(_baseApiUrl + "api/log", _logData);
                return _response.IsSuccessStatusCode;
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Exception: {exception.Message}");
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
                HttpResponseMessage _response = await this._httpClient.GetAsync(_baseApiUrl + "api/log");
                _response.EnsureSuccessStatusCode();
                List<ApiLogDto> _apiLogs = await _response.Content.ReadFromJsonAsync<List<ApiLogDto>>(_jsonOptions);
                List<ApiLogDto> _filteredApiLogs = _apiLogs.FindAll(log => log._action_type == actionType.ToString());
                return this.ConvertApiLogsToLogEntryModels(_filteredApiLogs);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Exception: {exception.Message}");
                return new List<LogEntryModel>();
            }
        }

        /// <summary>
        /// Gets logs matching specific parameters without filtering by user ID.
        /// </summary>
        /// <param name="actionType">The action type to filter by.</param>
        /// <param name="beforeTimestamp">The timestamp to filter by.</param>
        /// <returns>A task representing the asynchronous operation with a list of log entries.</returns>
        public async Task<List<LogEntryModel>> GetLogsWithParametersWithoutUserId(ActionType actionType, DateTime beforeTimestamp)
        {
            try
            {
                HttpResponseMessage _response = await this._httpClient.GetAsync(_baseApiUrl + "api/log");
                _response.EnsureSuccessStatusCode();
                List<ApiLogDto> _apiLogs = await _response.Content.ReadFromJsonAsync<List<ApiLogDto>>(_jsonOptions);
                List<ApiLogDto> _filteredApiLogs = _apiLogs.FindAll(log =>
                    log._action_type == actionType.ToString() &&
                    log._timestamp < beforeTimestamp);
                return this.ConvertApiLogsToLogEntryModels(_filteredApiLogs);
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
        /// <param name="beforeTimestamp">The timestamp to filter by.</param>
        /// <returns>A task representing the asynchronous operation with a list of log entries.</returns>
        public async Task<List<LogEntryModel>> GetLogsWithParameters(int userId, ActionType actionType, DateTime beforeTimestamp)
        {
            try
            {
                HttpResponseMessage _response = await this._httpClient.GetAsync(_baseApiUrl + "api/log");
                _response.EnsureSuccessStatusCode();
                List<ApiLogDto> _apiLogs = await _response.Content.ReadFromJsonAsync<List<ApiLogDto>>(_jsonOptions);
                List<ApiLogDto> _filteredApiLogs = _apiLogs.FindAll(log =>
                    log._user_id == userId &&
                    log._action_type == actionType.ToString() &&
                    log._timestamp < beforeTimestamp);
                return this.ConvertApiLogsToLogEntryModels(_filteredApiLogs);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Exception: {exception.Message}");
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

            List<LogEntryModel> result = new List<LogEntryModel>();

            foreach (ApiLogDto apiLog in apiLogs)
            {
                if (Enum.TryParse<ActionType>(apiLog._action_type, out ActionType actionType))
                {
                    result.Add(new LogEntryModel(
                        apiLog._log_id,
                        apiLog._user_id,
                        actionType,
                        apiLog._timestamp
                    ));
                }
            }

            return result;
        }
    }
}
