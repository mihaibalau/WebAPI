// <copyright file="ILoggerViewModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WinUI.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using WinUI.Model;

    /// <summary>
    /// Interface for LoggerViewModel:
    /// The view model for managing and displaying system logs.
    /// </summary>
    public interface ILoggerViewModel
    {
        /// <summary>
        /// Gets the collection of log entries to display.
        /// </summary>
        ObservableCollection<LogEntryModel> logs { get; }

        /// <summary>
        /// Gets the command to load all logs.
        /// </summary>
        ICommand loadAllLogsCommand { get; }

        /// <summary>
        /// Gets the command to filter logs by user ID.
        /// </summary>
        ICommand filterLogsByUserIdCommand { get; }

        /// <summary>
        /// Gets the command to filter logs by timestamp.
        /// </summary>
        ICommand filterLogsByTimestampCommand { get; }

        /// <summary>
        /// Gets the command to filter logs by action type.
        /// </summary>
        ICommand filterLogsByActionTypeCommand { get; }

        /// <summary>
        /// Gets the command to apply all filters simultaneously.
        /// </summary>
        ICommand applyAllFiltersCommand { get; }

        /// <summary>
        /// Gets the list of available action types for filtering.
        /// </summary>
        List<ActionType> actionTypes { get; }

        /// <summary>
        /// Gets or sets the user ID input for filtering.
        /// </summary>
        string userIdInput { get; set; }

        /// <summary>
        /// Gets or sets the selected action type for filtering.
        /// </summary>
        ActionType selectedActionType { get; set; }

        /// <summary>
        /// Gets or sets the selected timestamp for filtering.
        /// </summary>
        DateTime selectedTimestamp { get; set; }
    }
}
