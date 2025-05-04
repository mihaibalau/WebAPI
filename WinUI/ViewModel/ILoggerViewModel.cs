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
        ICommand load_all_logs_command { get; }

        /// <summary>
        /// Gets the command to filter logs by user ID.
        /// </summary>
        ICommand filter_logs_by_user_id_command { get; }

        /// <summary>
        /// Gets the command to filter logs by timestamp.
        /// </summary>
        ICommand filter_logs_by_timestamp_command { get; }

        /// <summary>
        /// Gets the command to filter logs by action type.
        /// </summary>
        ICommand filter_logs_by_action_type_command { get; }

        /// <summary>
        /// Gets the command to apply all filters simultaneously.
        /// </summary>
        ICommand apply_all_filters_command { get; }

        /// <summary>
        /// Gets the list of available action types for filtering.
        /// </summary>
        List<ActionType> action_types { get; }

        /// <summary>
        /// Gets or sets the user ID input for filtering.
        /// </summary>
        string user_id_input { get; set; }

        /// <summary>
        /// Gets or sets the selected action type for filtering.
        /// </summary>
        ActionType selected_action_type { get; set; }

        /// <summary>
        /// Gets or sets the selected timestamp for filtering.
        /// </summary>
        DateTime selected_timestamp { get; set; }
    }
}
