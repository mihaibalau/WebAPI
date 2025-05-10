using ClassLibrary.Domain;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace WinUI.ViewModel
{
    /// <summary>
    /// Defines the contract for the Notification ViewModel.
    /// </summary>
    public interface INotificationViewModel
    {
        /// <summary>
        /// Gets the collection of notifications.
        /// </summary>
        ObservableCollection<Notification> _notifications_collection { get; }

        /// <summary>
        /// Gets or sets the user ID associated with the notifications.
        /// </summary>
        int _user_id { get; set; }

        /// <summary>
        /// Asynchronously loads the notifications for the specified user ID.
        /// </summary>
        /// <param name="_user_id">The user ID for which to load the notifications.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task loadNotificationsAsync(int _user_id);

        /// <summary>
        /// Asynchronously deletes the notification for a given notification ID and user ID.
        /// </summary>
        /// <param name="_notification_id">The ID of the notification to delete.</param>
        /// <param name="_user_id">The user ID associated with the notification to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task deleteNotificationAsync(int _notification_id, int _user_id);

        /// <summary>
        /// Asynchronously finds a notification by its ID for a given user ID.
        /// </summary>
        /// <param name="notification_id">The ID of the notification to find.</param>
        /// <returns>A task that returns the found notification, or null if not found.</returns>
        Task<Notification> findNotificationByIdAsync(int _notification_id);
    }
}
