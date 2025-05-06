// <copyright file="ILoggerRepository.cs"  company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WinUI.Model;

namespace WinUI.Repository
{
    /// <summary>
    /// Interface for logger service operations.
    /// </summary>
    public interface ILoggerRepository
    {
        /// <summary>
        /// Gets all logs from the system.
        /// </summary>
        /// <returns>A task representing the asynchronous operation with a list of log entries.</returns>
        Task<List<LogEntryModel>> getAllLogs();

        /// <summary>
        /// Gets logs for a specific user.
        /// </summary>
        /// <param name="userId">The ID of the user whose logs to retrieve.</param>
        /// <returns>A task representing the asynchronous operation with a list of log entries.</returns>
        Task<List<LogEntryModel>> getLogsByUserId(int userId);

        /// <summary>
        /// Gets logs by action type.
        /// </summary>
        /// <param name="actionType">The action type to filter by.</param>
        /// <returns>A task representing the asynchronous operation with a list of log entries.</returns>
        Task<List<LogEntryModel>> getLogsByActionType(ActionType actionType);

        /// <summary>
        /// Gets logs from before a specific timestamp.
        /// </summary>
        /// <param name="timestamp">The timestamp to filter by.</param>
        /// <returns>A task representing the asynchronous operation with a list of log entries.</returns>
        Task<List<LogEntryModel>> getLogsBeforeTimestamp(DateTime timestamp);

        /// <summary>
        /// Gets logs matching specific parameters.
        /// </summary>
        /// <param name="userId">The user ID to filter by.</param>
        /// <param name="actionType">The action type to filter by.</param>
        /// <param name="timestamp">The timestamp to filter by.</param>
        /// <returns>A task representing the asynchronous operation with a list of log entries.</returns>
        Task<List<LogEntryModel>> getLogsWithParameters(int userId, ActionType actionType, DateTime timestamp);

        /// <summary>
        /// Gets logs matching specific parameters without filtering by user ID.
        /// </summary>
        /// <param name="actionType">The action type to filter by.</param>
        /// <param name="timestamp">The timestamp to filter by.</param>
        /// <returns>A task representing the asynchronous operation with a list of log entries.</returns>
        Task<List<LogEntryModel>> getLogsWithParametersWithoutUserId(ActionType actionType, DateTime timestamp);

        /// <summary>
        /// Records a new action in the log.
        /// </summary>
        /// <param name="userId">The ID of the user who performed the action.</param>
        /// <param name="actionType">The type of action performed.</param>
        /// <returns>A task representing the asynchronous operation with a boolean indicating success.</returns>
        Task<bool> logAction(int userId, ActionType actionType);
    }
}
