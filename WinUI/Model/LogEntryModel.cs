using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinUI.Model
{
    public enum ActionType
    {
        /// <summary>
        /// User login action.
        /// </summary>
        LOGIN,

        /// <summary>
        /// User logout action.
        /// </summary>
        LOGOUT,

        /// <summary>
        /// Profile update action.
        /// </summary>
        UPDATE_PROFILE,

        /// <summary>
        /// Password change action.
        /// </summary>
        CHANGE_PASSWORD,

        /// <summary>
        /// Account deletion action.
        /// </summary>
        DELETE_ACCOUNT,

        /// <summary>
        /// Account creation action.
        /// </summary>
        CREATE_ACCOUNT,
    }

    static class ActionTypeMethods
    {
        public static String convertToString(this ActionType actionType)
        {
            switch (actionType)
            {
                case ActionType.LOGIN:
                    return "LOGIN";
                case ActionType.LOGOUT:
                    return "LOGOUT";
                case ActionType.CREATE_ACCOUNT:
                    return "CREATE_ACCOUNT";
                default:
                    return "UNDEFINED";
            }
        }
    }

    /// <summary>
    /// Represents a log entry in the system's activity log.
    /// <param name="_log_id">The unique identifier for the log entry.</param>
    /// <param name="_user_id">The user ID associated with the action.</param>
    /// <param name="_action_type">The type of action performed.</param>
    /// <param name="_timestamp">The date and time when the action occurred.</param>
    public class LogEntryModel(int _log_id, int _user_id, ActionType _action_type, DateTime _timestamp)
    {
        /// <summary>
        /// Gets or sets the unique identifier for the log entry.
        /// </summary>
        public int log_id { get; set; } = _log_id;

        /// <summary>
        /// Gets or sets the ID of the user who performed the action.
        /// </summary>
        public int user_id { get; set; } = _user_id;
      
        /// <summary>
        /// Gets or sets the type of action performed.
        /// </summary>
        public ActionType action_type { get; set; } = _action_type;

        /// <summary>
        /// Gets or sets the date and time when the action was performed.
        /// </summary>
        public DateTime timestamp { get; set; } = _timestamp;

        /// <summary>
        /// Returns a string representation of the log entry.
        /// </summary>
        /// <returns>A string containing the log entry details.</returns>
        public override string ToString()
        {
            return $"LogId: {this.log_id}, UserId: {this.user_id}, ActionType: {this.action_type}, Timestamp: {this.timestamp}";
        }
    }
}