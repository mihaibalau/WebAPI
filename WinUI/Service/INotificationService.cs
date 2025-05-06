using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary.Domain;
using ClassLibrary.IRepository;
using ClassLibrary;

namespace WinUI.Service

{
    public interface INotificationService
    {
        /// <summary>
        /// Return all Notifications for all users.
        /// </summary>
        Task<List<Notification>> GetAllNotificationsAsync();

        /// <summary>
        /// Return all notifications for a specific user.
        /// </summary>
        Task<List<Notification>> GetNotificationsByUserIdAsync(int userId);

        /// <summary>
        /// Delete all notifications with <paramref name="notificationId"/>, 
        /// Only if is from user <paramref name="userId"/>.
        /// </summary>
        Task DeleteNotificationAsync(int notificationId, int userId);
    }
}
