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
        private readonly HttpClient _client;
        private readonly string _base_api_url;
        private readonly JsonSerializerOptions _json_options;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerProxy"/> class.
        /// </summary>
        public LoggerProxy()
        {
            this._client = new HttpClient();
            this._base_api_url = "http://localhost:5005";

            this._json_options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        /// <summary>
        /// Gets all logs from the database.
        /// </summary>
        /// <returns>A task representing the asynchronous operation with a list of log entries.</returns>
        public async Task<List<LogEntryModel>> getAllLogs()
        {
            try
            {
                // The ApiLogDto is a temporary class to handle the API response
                HttpResponseMessage response = await this._client.GetAsync($"{this._base_api_url}/api/log");
                response.EnsureSuccessStatusCode();

                List<ApiLogDto> api_logs = await response.Content.ReadFromJsonAsync<List<ApiLogDto>>(this._json_options);
                return this.convertApiLogsToLogEntryModels(api_logs);
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
        /// <param name="user_id">The ID of the user whose logs to retrieve.</param>
        /// <returns>A task representing the asynchronous operation with a list of log entries.</returns>
        public async Task<List<LogEntryModel>> getLogsByUserId(int user_id)
        {
            try
            {
                HttpResponseMessage response = await this._client.GetAsync($"{this._base_api_url}/api/log");
                response.EnsureSuccessStatusCode();
                List<ApiLogDto> api_logs = await response.Content.ReadFromJsonAsync<List<ApiLogDto>>(this._json_options);
                List<ApiLogDto> filtered_api_logs = api_logs.FindAll(log => log.userId == user_id);
                return this.convertApiLogsToLogEntryModels(filtered_api_logs);
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
        /// <param name="before_timestamp">The timestamp to filter by.</param>
        /// <returns>A task representing the asynchronous operation with a list of log entries.</returns>
        public async Task<List<LogEntryModel>> getLogsBeforeTimestamp(DateTime before_timestamp)
        {
            try
            {
                HttpResponseMessage response = await this._client.GetAsync($"{this._base_api_url}/api/log");
                response.EnsureSuccessStatusCode();
                List<ApiLogDto> api_logs = await response.Content.ReadFromJsonAsync<List<ApiLogDto>>(this._json_options);
                List<ApiLogDto> filtered_api_logs = api_logs.FindAll(log => log.timestamp < before_timestamp);
                return this.convertApiLogsToLogEntryModels(filtered_api_logs);
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
        /// <param name="user_id">The ID of the user who performed the action.</param>
        /// <param name="action_type">The type of action performed.</param>
        /// <returns>A task representing the asynchronous operation with a boolean indicating success.</returns>
        public async Task<bool> logAction(int user_id, ActionType action_type)
        {
            try
            {
                ApiLogDto logData = new ApiLogDto
                {
                    userId = user_id,
                    actionType = action_type.ToString(),
                    timestamp = DateTime.UtcNow
                };

                HttpResponseMessage response = await this._client.PostAsJsonAsync($"{this._base_api_url}/api/log", logData);
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
        /// <param name="action_type">The action type to filter by.</param>
        /// <returns>A task representing the asynchronous operation with a list of log entries.</returns>
        public async Task<List<LogEntryModel>> getLogsByActionType(ActionType action_type)
        {
            try
            {
                HttpResponseMessage response = await this._client.GetAsync($"{this._base_api_url}/api/log");
                response.EnsureSuccessStatusCode();
                List<ApiLogDto> api_logs = await response.Content.ReadFromJsonAsync<List<ApiLogDto>>(this._json_options);
                List<ApiLogDto> filtered_api_logs = api_logs.FindAll(log => log.actionType == action_type.ToString());
                return this.convertApiLogsToLogEntryModels(filtered_api_logs);
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
        /// <param name="action_type">The action type to filter by.</param>
        /// <param name="before_timestamp">The timestamp to filter by.</param>
        /// <returns>A task representing the asynchronous operation with a list of log entries.</returns>
        public async Task<List<LogEntryModel>> getLogsWithParametersWithoutUserId(ActionType action_type, DateTime before_timestamp)
        {
            try
            {
                HttpResponseMessage response = await this._client.GetAsync($"{this._base_api_url}/api/log");
                response.EnsureSuccessStatusCode();
                List<ApiLogDto> api_logs = await response.Content.ReadFromJsonAsync<List<ApiLogDto>>(this._json_options);
                List<ApiLogDto> filtered_api_logs = api_logs.FindAll(log => 
                    log.actionType == action_type.ToString() &&
                    log.timestamp < before_timestamp);
                return this.convertApiLogsToLogEntryModels(filtered_api_logs);
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
        /// <param name="user_id">The user ID to filter by.</param>
        /// <param name="action_type">The action type to filter by.</param>
        /// <param name="before_timestamp">The timestamp to filter by.</param>
        /// <returns>A task representing the asynchronous operation with a list of log entries.</returns>
        public async Task<List<LogEntryModel>> getLogsWithParameters(int user_id, ActionType action_type, DateTime before_timestamp)
        {
            try
            {
                HttpResponseMessage response = await this._client.GetAsync($"{this._base_api_url}/api/log");
                response.EnsureSuccessStatusCode();
                List<ApiLogDto> api_logs = await response.Content.ReadFromJsonAsync<List<ApiLogDto>>(this._json_options);
                List<ApiLogDto> filtered_api_logs = api_logs.FindAll(log =>
                    log.userId == user_id &&
                    log.actionType == action_type.ToString() &&
                    log.timestamp < before_timestamp);
                return this.convertApiLogsToLogEntryModels(filtered_api_logs);
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
        private List<LogEntryModel> convertApiLogsToLogEntryModels(List<ApiLogDto> api_logs)
        {
            if (api_logs == null)
                return new List<LogEntryModel>();

            var result = new List<LogEntryModel>();

            foreach (var api_log in api_logs)
            {
                if (Enum.TryParse<ActionType>(api_log.actionType, out ActionType _action_type))
                {
                    result.Add(new LogEntryModel(
                        api_log.logId,
                        api_log.userId,
                        _action_type,
                        api_log.timestamp
                    ));
                }
            }

            return result;
        }
    }

    public class ApiLogDto
    {
        [JsonPropertyName("logId")]
        public int logId { get; set; }
        [JsonPropertyName("userId")]
        public int userId { get; set; }
        [JsonPropertyName("actionType")]
        public string actionType { get; set; }
        [JsonPropertyName("timestamp")]
        public DateTime timestamp { get; set; }
    }
}

