// <copyright file="LoggerService.cs"  company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WinUI.Service
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WinUI.Model;
    using WinUI.Repository;

    /// <summary>
    /// Model for handling system logging operations.
    /// </summary>
    public class LoggerService : ILoggerService
    {
        private readonly ILoggerRepository _logger_repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerService"/> class.
        /// </summary>
        /// <param name="_logger_repository">The logger service interface.</param>
        public LoggerService(ILoggerRepository _logger_repository)
        {
            this._logger_repository = _logger_repository ?? throw new ArgumentNullException(nameof(_logger_repository));
        }

        /// <summary>
        /// Retrieves all log entries from the system.
        /// </summary>
        /// <returns>A list of all log entries.</returns>
        public async Task<List<LogEntryModel>> getAllLogs()
        {
            return await this._logger_repository.getAllLogs();
        }

        /// <summary>
        /// Retrieves log entries for a specific user.
        /// </summary>
        /// <param name="_user_id">The ID of the user.</param>
        /// <returns>A list of log entries for the specified user.</returns>
        /// <exception cref="ArgumentException">Thrown when the _user_id is invalid.</exception>
        public async Task<List<LogEntryModel>> getLogsByUserId(int _user_id)
        {
            if (_user_id <= 0)
            {
                throw new ArgumentException("User ID must be greater than zero.", nameof(_user_id));
            }

            return await this._logger_repository.getLogsByUserId(_user_id);
        }

        /// <summary>
        /// Retrieves log entries for a specific action type.
        /// </summary>
        /// <param name="_action_type">The type of action to filter by.</param>
        /// <returns>A list of log entries for the specified action type.</returns>
        /// <exception cref="ArgumentNullException">This exception won't occur since _action_type is an enum, but kept for interface compliance.</exception>
        public async Task<List<LogEntryModel>> getLogsByActionType(ActionType _action_type)
        {
            // _action_type is an enum, so it can't be null, but we're keeping this check
            // for interface compliance and future-proofing
            // The result of the expression is always 'false'
            if (_action_type == null)
            {
                throw new ArgumentNullException(nameof(_action_type), "Action type cannot be null.");
            }

            return await this._logger_repository.getLogsByActionType(_action_type);
        }

        /// <summary>
        /// Retrieves log entries before a specific _timestamp.
        /// </summary>
        /// <param name="_timestamp">The cutoff _timestamp.</param>
        /// <returns>A list of log entries before the specified _timestamp.</returns>
        /// <exception cref="ArgumentException">Thrown when the _timestamp is default/uninitialized.</exception>
        public async Task<List<LogEntryModel>> getLogsBeforeTimestamp(DateTime _timestamp)
        {
            if (_timestamp == default)
            {
                throw new ArgumentException("_timestamp cannot be default.", nameof(_timestamp));
            }

            return await this._logger_repository.getLogsBeforeTimestamp(_timestamp);
        }

        /// <summary>
        /// Retrieves log entries filtered by multiple parameters.
        /// </summary>
        /// <param name="_user_id">The ID of the user, or null for all users.</param>
        /// <param name="_action_type">The type of action.</param>
        /// <param name="_timestamp">The cutoff _timestamp.</param>
        /// <returns>A list of log entries matching the specified filters.</returns>
        public async Task<List<LogEntryModel>> getLogsWithParameters(int? _user_id, ActionType _action_type, DateTime _timestamp)
        {
            if (_user_id != null)
            {
                return await this._logger_repository.getLogsWithParameters(_user_id.Value, _action_type, _timestamp);
            }
            else
            {
                return await this._logger_repository.getLogsWithParametersWithoutUserId(_action_type, _timestamp);
            }
        }

        /// <summary>
        /// Logs a user action in the system.
        /// </summary>
        /// <param name="_user_id">The ID of the user performing the action.</param>
        /// <param name="_action_type">The type of action being performed.</param>
        /// <returns>True if the action was logged successfully, otherwise false.</returns>
        /// <exception cref="ArgumentException">Thrown when the _user_id is invalid.</exception>
        /// <exception cref="ArgumentNullException">This exception won't occur since action_type is an enum, but kept for interface compliance.</exception>
        public async Task<bool> logAction(int _user_id, ActionType _action_type)
        {
            if (_user_id <= 0)
            {
                throw new ArgumentException("User ID must be greater than zero.", nameof(_user_id));
            }

            // action_type is an enum, so it can't be null, but we're keeping this check
            // for interface compliance and future-proofing
            if (_action_type == null)
            {
                throw new ArgumentNullException(nameof(_action_type), "Action type cannot be null.");
            }

            return await this._logger_repository.logAction(_user_id, _action_type);
        }
    }
}
